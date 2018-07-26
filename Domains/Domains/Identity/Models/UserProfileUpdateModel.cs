using AutoMapper;
using Domains.Identity.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domains.Identity.Models
{
    public class UserProfileUpdateModel
    {
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }

    public class UserProfileUpdateModelMapper : Profile
    {
        public UserProfileUpdateModelMapper()
        {
            CreateMap<UserProfileUpdateModel, User>();
        }
    }

    public class UserProfileUpdateModelValidator : AbstractValidator<UserProfileUpdateModel>
    {
        public UserProfileUpdateModelValidator()
        {
            RuleFor(p => p.Email).NotEmpty().EmailAddress();
            RuleFor(p => p.PhoneNumber).NotEmpty();
        }
    }
}
