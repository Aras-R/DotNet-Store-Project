using Store.Domain.Entities.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Entities.HomePages
{
    public class HomePageImages:BaseEntity
    {
        public string Src { get; set; }
        public string link { get; set; }
        public ImageLocation  ImageLocation { get; set; }
    }

    public enum ImageLocation
    {
        IconImages= 0,
        FourImageCenter = 1,
        TwoImageCenter = 2,
        CenterFullScreen= 3,
    }

}
