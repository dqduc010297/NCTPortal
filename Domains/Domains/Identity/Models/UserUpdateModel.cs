using AutoMapper;
using Domains.Identity.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domains.Identity.Models
{
    public class UserUpdateModel
    {
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Role { get; set; }
    }

    public class UserUpdateModelMapper: Profile
    {
        public UserUpdateModelMapper()
        {
            CreateMap<UserUpdateModel, User>()
                .ForSourceMember(s => s.Role, opt => opt.Ignore());
        }
    }

    public class UserUpdateModelValidator : AbstractValidator<UserUpdateModel>
    {
        public UserUpdateModelValidator()
        {
            RuleFor(p => p.Email).NotEmpty().EmailAddress();
            RuleFor(p => p.PhoneNumber).NotEmpty();
            RuleFor(p => p.Role).NotEmpty();
        }
    }
}
