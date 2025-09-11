using System;

namespace Store.Application.Services.HomePages.Queries.GetSliders
{
    public class SliderDto
    {
        public long Id { get; set; }
        public string Src { get; set; }   
        public string Link { get; set; } 
        public DateTime InsertTime { get; set; }
    }
}
