using System;

namespace DTOModel
{
    public class Dto_Math_FileUpload
    {
        public Guid UploadId { get; set; }
        public DateTime UploadTime { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string FileType { get; set; }
    }
}
