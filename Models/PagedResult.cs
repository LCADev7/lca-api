﻿namespace Church.Models
{
    public class PagedResult<T>
    {
        public List<T> Results { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalNumberOfPages { get; set; }
        public int TotalNumberOfRecords { get; set; }
    }
}
