﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PerfumeStore.Service.BusinessModel.CustomResponse
{
    public static class ErrorResp
    {
        public static IActionResult InternalServerError(string? message)
        {
            return new JsonResult(new { Error = message ?? RespMsg.INTERNAL_SERVER_ERROR }) { StatusCode = RespCode.INTERNAL_SERVER_ERROR };
        }

        public static IActionResult NotFound(string? message)
        {
            return new JsonResult(new { Error = message ?? RespMsg.NOT_FOUND }) { StatusCode = RespCode.NOT_FOUND };
        }

        public static IActionResult BadRequest(string? message)
        {
            return new JsonResult(new { Error = message ?? RespMsg.BAD_REQUEST }) { StatusCode = RespCode.BAD_REQUEST };
        }
        public static IActionResult Unauthorized(string? message)
        {
            return new JsonResult(new { Error = message ?? RespMsg.UNAUTHORIZED }) { StatusCode = RespCode.UNAUTHORIZED };
        }

        public static IActionResult Forbidden(string? message)
        {
            return new JsonResult(new { Error = message ?? RespMsg.FORBIDDEN }) { StatusCode = RespCode.FORBIDDEN };
        }

    }
}