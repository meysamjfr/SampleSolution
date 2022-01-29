﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Project.Models
{
    public class Response<T>
    {
        public Response(ResponseStatus status)
        {
            _setStatus(status);
        }

        public Response(ResponseStatus status, List<string> errors)
        {
            _setStatus(status);
            Errors = errors;
        }

        public Response(ResponseStatus status, T data)
        {
            _setStatus(status);
            Data = data;
        }

        public Response(ResponseStatus status, List<string> errors, T data)
        {
            _setStatus(status);
            Errors = errors;
            Data = data;
        }

        public ResponseStatus Status { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
        public T Data { get; set; }

        private int _statusCode;

        public Response<T> GetResponse()
        {
            return this;
        }

        public JsonResult GetJsonResult()
        {
            return new(this) { StatusCode = _statusCode };
        }

        public ActionResult<T> GetActionResult()
        {
            return new(Data);
        }

        private void _setStatus(ResponseStatus status)
        {
            Status = status;

            switch (status)
            {
                case ResponseStatus.Failed:
                    Message = "خطا در تکمیل درخواست";
                    _statusCode = StatusCodes.Status500InternalServerError;
                    break;
                case ResponseStatus.Succeed:
                    Message = "با موفقیت انجام شد";
                    _statusCode = StatusCodes.Status200OK;
                    break;
                case ResponseStatus.NotFound:
                    Message = "اطلاعات مورد نظر یافت نشد";
                    _statusCode = StatusCodes.Status404NotFound;
                    break;
                case ResponseStatus.BadRequest:
                    Message = "خطا در اطلاعات ارسالی";
                    _statusCode = StatusCodes.Status400BadRequest;
                    break;
                case ResponseStatus.Unauthorized:
                    Message = "کاربر نامعتبر";
                    _statusCode = StatusCodes.Status401Unauthorized;
                    break;
                case ResponseStatus.Forbidden:
                    Message = "اجازه دسترسی نامعتبر";
                    _statusCode = StatusCodes.Status403Forbidden;
                    break;
                default:
                    break;
            }
        }
    }
}
