using System;

namespace Store.Application.Services.HomePages.Queries.GetImagesAdmin
{
    public class ImageAdminDto
    {
        public long Id { get; set; }
        public string Src { get; set; }
        public string Link { get; set; }
        public string ImageLocation { get; set; }
        public DateTime InsertTime { get; set; }
    }
}
