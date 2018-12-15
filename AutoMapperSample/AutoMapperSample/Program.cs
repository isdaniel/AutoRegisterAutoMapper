using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ExtensionTool;
namespace AutoMapperSample
{
    public class UserInfoModel
    {
        public int RowId { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }

    public class UserInfoViewModel
    {
        public string Detail { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            UserInfoModel userInfo = new UserInfoModel() {
                RowId = 1,
                Name = "Daniel",
                Age = 10
            };

           var profiles =  Assembly.GetExecutingAssembly()
                                   .GetInstancesByAssembly<Profile>();

            foreach (var profile in profiles)
            {
                Mapper.Initialize(x => x.AddProfile(profile));
            }

            var viewModel = Mapper.Map<UserInfoViewModel>(userInfo);

            Console.ReadKey();
        }
    }

    public class UserInfoProfile : Profile
    {
        public UserInfoProfile()
        {
            CreateMap<UserInfoModel, UserInfoViewModel>()
                    .ForMember(t => t.Detail, 
                                    s => s.MapFrom(_ => $"DetailInfo:{_.Name} {_.Age}"));
        }
    }
}
