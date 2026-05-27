using StudentManager.Shared.Enums;

namespace StudentManager.Shared.Models
{
    public class FileEntry
    {
        // Another data container, we must first think about what data we need when we want to upload a file. 
        /*need a fileId first, id always important esepcialy for DB 
         * name of the file- string
         * storedfiename- string - this is the name of the file when wes store it in the server, we need it to be uniq so we can use Guid.Newuid() + extension
         * UploadedAt-- DateTime
         * SubjectId -- shoud i make it nullable? not all fies are related to subjects
         * FileType -- enum (image, document, video, other)
         * SectionType -- enum (homework, lecture, other) this is for the UI to know where to show the file.
         * 
         */
        public string? Name { get; set; }
        public int FileEntryId { get; set; }
        public string? StoredFileName { get; set; }
        public DateTime UploadedAt { get; set; }
        public int? SubjectId { get; set; }
        public FileTypeEnum FileType { get; set; }
        public SectionTypeEnum SectionType { get; set; }
    }
}
