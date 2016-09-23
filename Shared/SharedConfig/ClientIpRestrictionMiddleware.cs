//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;
//using log4net;
//using Microsoft.Owin;

//namespace SharedConfig
//{
//    public class ClientIpRestrictionMiddleware : OwinMiddleware
//    {
//        private ILog logger = log4net.LogManager.GetLogger("IpFilter");
//        /// <summary>
//        /// hold the latest rules loaded from database
//        /// </summary>
//        private ClientIpWhiteListModels[] rules;

//        /// <summary>
//        /// when a ip is on checking, and meanwhile a db reload is on processing, for performance, will use this fallback rules though it's not the 
//        /// latest one from db.
//        /// </summary>
//        private ClientIpWhiteListModels[] fallbackRules = null;
//        private int onChecking = 0;
//        private bool needReloadFallbackRules = false;
//        /// <summary>
//        /// reload the rules from db periodically.
//        /// </summary>
//        private readonly Timer reloadTimer = new Timer();
//        public ClientIpRestrictionMiddleware(OwinMiddleware next) :
//            base(next)
//        {
//            this.reloadTimer.Elapsed += (a, b) =>
//            {
//                if (0 == Interlocked.CompareExchange(ref onChecking, 1, 0))
//                {
//                    logger.Info("Reloading rules by schedule...");
//                    using (var db = new ApplicationDbContext())
//                    {
//                        rules = db.ClientIpRestrictionModels.ToArray();
//                    }

//                    needReloadFallbackRules = true;
//                    onChecking = 0;
//                    logger.Info("Reloaded rules by schedule done.");
//                }
//            };
//            //this.reloadTimer.Interval = 10 * 60 * 1000;
//            this.reloadTimer.Interval = 1 * 60 * 1000;
//            this.reloadTimer.Start();
//        }

//        public override Task Invoke(IOwinContext context)
//        {
//            // first incoming a request, and ip rule table is empty, load it instantly
//            if (this.rules == null)
//            {
//                while (true)
//                {
//                    logger.Debug("Trying to init rules for incoming first request...");
//                    if (0 == Interlocked.CompareExchange(ref this.onChecking, 1, 0))
//                    {
//                        try
//                        {
//                            if (this.rules == null)
//                            {
//                                logger.Debug("Loading rules from db for incoming first request...");
//                                using (var db = new ApplicationDbContext())
//                                {
//                                    this.rules = db.ClientIpRestrictionModels.ToArray();
//                                }
//                            }

//                            this.fallbackRules = rules;
//                            this.needReloadFallbackRules = false;
//                            break;
//                        }
//                        catch (Exception ex)
//                        {
//                            logger.Error("Failed to loading rules from db for incoming first request");
//                            break;
//                        }
//                        finally
//                        {
//                            this.onChecking = 0;
//                        }
//                    }

//                    // spin
//                    Thread.Sleep(500);
//                }
//            }

//            //either on rules or fallbackRules
//            ClientIpWhiteListModels[] operatingRules = null;
//            if (0 == Interlocked.CompareExchange(ref onChecking, 1, 0))
//            {
//                try
//                {
//                    // copy it
//                    operatingRules = rules;
//                    if (this.needReloadFallbackRules)
//                    {
//                        logger.Debug("Reloading fallbackRules");
//                        // update the fallbackRules to latest one.
//                        fallbackRules = rules;
//                        this.needReloadFallbackRules = false;
//                    }
//                }
//                finally
//                {
//                    onChecking = 0;
//                }
//            }
//            else
//            {
//                logger.Info("using fallbackRules to validate request(for fast service)");
//                // rules is under updating, don't wait it, use the fallback one.
//                operatingRules = fallbackRules;
//            }

//            if (this.Proceed(context.Request, operatingRules))
//            {
//                return Next.Invoke(context);
//            }
//            else
//            {
//                context.Response.StatusCode = 403;
//                return Task.FromResult(0);
//            }
//        }

//        /// <summary>
//        /// Test if the client ip is allowed or denied from the white list.
//        /// </summary>
//        /// <param name="request">request send from client side</param>
//        /// <param name="whiteList">active white list</param>
//        /// <returns>true for let go, otherwise block.</returns>
//        private bool Proceed(IOwinRequest request, IEnumerable<ClientIpWhiteListModels> whiteList)
//        {

//#if DEBUG
//            string remoteIp = request.Headers["FakeIp"] ?? request.RemoteIpAddress;
//#else
//            string remoteIp = request.RemoteIpAddress;
//#endif

//            var remoteIpParts = remoteIp.Split('.').Select(i => int.Parse(i)).ToArray();

//            #region first check deny list

//            bool denied = false;
//            foreach (var oneDenyRule in whiteList.Where(p => p.Action == IpRestrictionAction.Deny))
//            {
//                if (remoteIp == oneDenyRule.RestrictedIpFrom
//                    || remoteIp == oneDenyRule.RestrictedIpTo)
//                {
//                    denied = true;
//                    break;
//                }

//                if (!string.IsNullOrEmpty(oneDenyRule.RestrictedIpTo))
//                {
//                    var from = oneDenyRule.RestrictedIpFrom.Split('.').Select(i => int.Parse(i)).ToArray();
//                    var to = oneDenyRule.RestrictedIpTo.Split('.').Select(i => int.Parse(i)).ToArray();
//                    for (var i = 0; i < remoteIpParts.Length; i++)
//                    {
//                        if (remoteIpParts[i] < from[i] || remoteIpParts[i] > to[i])
//                        {
//                            goto nextDenyRule;
//                        }
//                    }

//                    denied = true;
//                    break;
//                }

//            nextDenyRule:
//                ;
//            }

//            if (denied)
//            {
//                logger.Warn("Deny Ip: " + remoteIp);
//                return false;
//            }

//            #endregion


//            #region first check allow list

//            bool allowed = false;
//            foreach (var oneAllowRule in whiteList.Where(p => p.Action == IpRestrictionAction.Allow))
//            {
//                if (remoteIp == oneAllowRule.RestrictedIpFrom
//                    || remoteIp == oneAllowRule.RestrictedIpTo)
//                {
//                    allowed = true;
//                    break;
//                }

//                if (!string.IsNullOrEmpty(oneAllowRule.RestrictedIpTo))
//                {
//                    var from = oneAllowRule.RestrictedIpFrom.Split('.').Select(i => int.Parse(i)).ToArray();
//                    var to = oneAllowRule.RestrictedIpTo.Split('.').Select(i => int.Parse(i)).ToArray();
//                    for (var i = 0; i < remoteIpParts.Length; i++)
//                    {
//                        if (remoteIpParts[i] >= from[i] && remoteIpParts[i] <= to[i])
//                        {
//                            allowed = true;
//                            goto finished;
//                        }

//                        break;
//                    }
//                }
//            }
//        finished:
//            if (!allowed)
//            {
//                logger.Warn("No explicit allow Ip(default to deny): " + remoteIp);
//                return false;
//            }
//            else
//            {
//                logger.Info("Allow Ip: " + remoteIp);
//            }

//            #endregion

//            return true;
//        }
//    }
//}
