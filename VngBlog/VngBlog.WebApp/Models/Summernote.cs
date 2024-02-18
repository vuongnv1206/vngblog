﻿namespace VngBlog.WebApp.Models
{
    public class Summernote
    {
        public Summernote(string idEditor, bool loadLibrary = true)
        {
            IdEditor = idEditor;
            LoadLibrary = loadLibrary;
        }

        public string IdEditor { get; set; }

        public bool LoadLibrary { get; set; }

        public int Height { get; set; } = 120;

        public string Toolbar { get; set; } = @"
            [
                ['style', ['style']],
                ['font', ['bold', 'underline', 'clear']],
                ['color', ['color']],
                ['para', ['ul', 'ol', 'paragraph']],
                ['table', ['table']],
                ['insert', ['link', 'picture', 'video', 'elfinder']],
                ['height', ['height']],
                ['view', ['fullscreen', 'codeview', 'help']]
            ]       
        ";
    }
}
