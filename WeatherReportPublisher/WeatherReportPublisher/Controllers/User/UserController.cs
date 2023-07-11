﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WeatherReport.Domain.Mapper;
using WeatherReport.Domain.Service.User;
using WeatherReport.Domain.Service.User.Dto;
using WeatherReport.Domain.Service.User.Entities;
using WeatherReport.Domain.Token;
using WeatherReport.SharedKernel.Utils.Notifications;

namespace WeatherReport.Api.Controllers.User
{
    [ApiController]
    [Route("api/user")]
    public class UserController : Controller
    {
        private readonly INotification _notification;
        private readonly IUserService _userService;
        IMapper _mapper = AutoMapperProfile.Initialize();

        public UserController(INotification notification, IUserService userService)
        {
            _notification = notification;
            _userService = userService;
        }

        [HttpPost]
        public IActionResult Post(UserDto user)
        {
            var response = _userService.PostRegister(user);

            if (response == null)
                return BadRequest(_notification.GetNotifications());

            return Ok(response);
        }

        [HttpGet]
        //[Authorize]
        public IActionResult Get()
        {
            var response = _userService.Get();
            if (response == null)
                return BadRequest(_notification.GetNotifications());

            return Ok(response);
        }

        [HttpGet]
        [Route("id")]
        //[Authorize]
        public IActionResult GetById(int id)
        {
            var response = _userService.GetById(id);
            if (response == null)
                return BadRequest(_notification.GetNotifications());

            return Ok(response);
        }

        [HttpPost]
        [Route("login")]
        public IActionResult PostLogin(UserLoginDto model)
        {
            var user = _userService.PostLogin(model);
            if (user == null)
                return BadRequest(_notification.GetNotifications());

           var userEntity = _mapper.Map<UserEntity>(user);
            var token = TokenService.GenerateToken(userEntity);

            return Ok(token);
        }
    }
}
