using AutoMapper;
using IdentityManagementAPI.Models;
using IdentityManagementAPI.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityManagementAPI.DataMaper
{
    public class IdentityManagementAPIMapping : Profile
    {
       public IdentityManagementAPIMapping()
        {
            CreateMap<Subject, SubjectDto>().ReverseMap();
            CreateMap<Book, BookDto>().ReverseMap();
        }
    }
}
