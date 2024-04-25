using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ViewModel
{
    public class ResponseWrapper<TData>
    {
        #region Constructors

        public ResponseWrapper()
        {
        }

        public ResponseWrapper(bool success, string message, ErrorResponse error, TData data)
        {
            Success = success;
            Message = message;
            Error = error;
            Data = data;
        }

        #endregion Constructors

        #region Properties

        public bool Success { get; set; }
        public string Message { get; set; }
        public ErrorResponse Error { get; set; }
        public TData Data { get; set; }

        #endregion Properties
    }

    public class PageResponseWrapper<TData>
    {
        #region Constructors

        public PageResponseWrapper()
        {
        }

        public PageResponseWrapper(int totalPage, int totalRecordCount, int currentPage, int currentPageRecordCount, TData records)
        {
            TotalPage = totalPage;
            TotalRecordCount = totalRecordCount;
            CurrentPage = currentPage;
            CurrentPageRecordCount = currentPageRecordCount;
            Records = records;
        }

        #endregion Constructors

        #region Properties

        public int TotalPage { get; set; }
        public int TotalRecordCount { get; set; }
        public int CurrentPage { get; set; }
        public int CurrentPageRecordCount { get; set; }
        public TData Records { get; set; }

        #endregion Properties
    }

    public class ErrorResponse
    {
        #region Constructors

        public ErrorResponse()
        {
        }

        public ErrorResponse(string message)
        {
            ErrorCode = null;
            ErrorMessage = message;
        }

        public ErrorResponse(string code, string message)
        {
            ErrorCode = code;
            ErrorMessage = message;
        }

        #endregion Constructors

        #region Properties

        public string ErrorCode { get; set; }

        public string ErrorMessage { get; set; }

        #endregion Properties
    }
}
