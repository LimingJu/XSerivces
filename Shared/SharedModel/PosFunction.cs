using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedModel
{
    public enum PosUIConfigurationElementType { Button, TextBox }
    class PosUIConfiguration
    {
        public int Id { get; set; }

        public PosUIConfigurationElementType UIElementType { get; set; }
        public int PositionX { get; set; }
        public int PositionY { get; set; }

        public string ExecutionCommand { get; set; }
    }
}
