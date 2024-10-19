using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedParams.DTOs
{
    public class GenericObjDTO
    {
        public string ImageToDisplay {  get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public List<string> DisplayProps { get; set;} = new List<string>();
        public int Id { get; set; }
        public EnumGenericObjType EnumGenericObjType { get; set; }  

    }
}


public enum EnumGenericObjType
{
    Games,
    Hardware,
    Other
}