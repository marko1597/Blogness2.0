using System;
using System.Collections.Generic;
using System.Linq;
using Blog.DataAccess.Database.Entities.Objects;

namespace Blog.Logic.Core.Tests
{
    public class ObjectMocks
    {
        public List<EducationType> GenerateEducationTypes()
        {
            var educationTypes = new List<EducationType>
                                  {
                                      new EducationType
                                      {
                                          EducationTypeName = "Grade School"
                                      },
                                      new EducationType
                                      {
                                          EducationTypeName = "High School"
                                      },
                                      new EducationType
                                      {
                                          EducationTypeName = "College Education"
                                      },
                                      new EducationType
                                      {
                                          EducationTypeName = "Post Graduate"
                                      }
                                  };

            return educationTypes;
        }

        public List<Education> GenerateEducations()
        {
            var educations = new List<Education>();

            for (var i = 1; i <= 3; i++)
            {
                educations.Add(new Education
                {
                    EducationTypeId = 1,
                    UserId = i,
                    SchoolName = "Grade School",
                    City = "City",
                    State = "State",
                    Country = "Country",
                    YearAttended = DateTime.Now.AddYears(-20),
                    YearGraduated = DateTime.Now.AddYears(-14),
                    Course = string.Empty,
                    CreatedBy = i,
                    CreatedDate = DateTime.Now,
                    ModifiedBy = i,
                    ModifiedDate = DateTime.Now
                });

                educations.Add(new Education
                {
                    EducationTypeId = 2,
                    UserId = i,
                    SchoolName = "High School",
                    City = "City",
                    State = "State",
                    Country = "Country",
                    YearAttended = DateTime.Now.AddYears(-14),
                    YearGraduated = DateTime.Now.AddYears(-8),
                    Course = string.Empty,
                    CreatedBy = i,
                    CreatedDate = DateTime.Now,
                    ModifiedBy = i,
                    ModifiedDate = DateTime.Now
                });

                educations.Add(new Education
                {
                    EducationTypeId = 3,
                    UserId = i,
                    SchoolName = "College Education",
                    City = "City",
                    State = "State",
                    Country = "Country",
                    YearAttended = DateTime.Now.AddYears(-8),
                    YearGraduated = DateTime.Now.AddYears(-4),
                    Course = "BS Computer Science",
                    CreatedBy = i,
                    CreatedDate = DateTime.Now,
                    ModifiedBy = i,
                    ModifiedDate = DateTime.Now
                });
            }

            return educations;
        }

        public List<Address> GenerateAddresses()
        {
            var addresses = new List<Address>();

            for (var i = 1; i <= 3; i++)
            {
                addresses.Add(new Address
                {
                    UserId = i,
                    StreetAddress = "Street Address",
                    City = "City",
                    State = "State",
                    Country = "Country",
                    Zip = 1234
                });
            }

            return addresses;
        }

        public List<Hobby> GenerateHobbies()
        {
            var hobbies = new List<Hobby>();

            for (var i = 1; i <= 3; i++)
            {
                hobbies.Add(new Hobby
                {
                    HobbyName = "Fooing",
                    UserId = i,
                    CreatedBy = i,
                    CreatedDate = DateTime.Now,
                    ModifiedBy = i,
                    ModifiedDate = DateTime.Now
                });
            }

            return hobbies;
        }

        public List<Album> GenerateAlbums()
        {
            var albums = new List<Album>();

            for (var i = 1; i <= 3; i++)
            {
                albums.Add(new Album
                           {
                               AlbumName = "Foo",
                               UserId = i,
                               CreatedBy = i,
                               CreatedDate = DateTime.Now,
                               ModifiedBy = i,
                               ModifiedDate = DateTime.Now,
                               IsUserDefault = true
                           });
            }

            return albums;
        }

        public List<Media> GenerateMedia()
        {
            var mediae = new List<Media>();

            for (var i = 1; i <= 3; i++)
            {
                mediae.Add(new Media
                {
                    CustomName = Guid.NewGuid().ToString(),
                    CreatedBy = i,
                    CreatedDate = DateTime.Now,
                    ModifiedBy = i,
                    ModifiedDate = DateTime.Now,
                    AlbumId = i,
                    FileName = "foo.jpg",
                    MediaUrl = "http://mock.object/foo",
                    MediaType = "image/jpeg",
                    MediaPath = @"C:/bar/foo.jpg",
                    ThumbnailUrl = "http://mock.object/foo/thumb",
                    ThumbnailPath = @"C:/bar/tn/tn_foo.jpg",
                });
            }

            return mediae;
        }

        public List<User> GenerateUsers()
        {
            var addresses = GenerateAddresses();
            var educations = GenerateEducations();
            var hobbies = GenerateHobbies();

            var users = new List<User>
                        {
                            new User
                            {
                                UserId = 1,
                                FirstName = "Jason",
                                LastName = "Magpantay",
                                UserName = "jama",
                                Password = "testtest1",
                                EmailAddress = "jason.magpantay@gmail.com",
                                BirthDate = DateTime.Now.AddYears(-25),
                                PictureId = 1,
                                BackgroundId = 1,
                                Address = addresses.Find(a => a.UserId == 1),
                                Education = educations.Where(a => a.UserId == 1).ToList(),
                                Hobbies = hobbies.Where(a => a.UserId == 1).ToList()
                            },
                            new User
                            {
                                UserId = 2,
                                FirstName = "Jason",
                                LastName = "Avel",
                                UserName = "jaav",
                                Password = "testtest1",
                                EmailAddress = "jason.avel@gmail.com",
                                BirthDate = DateTime.Now.AddYears(-25),
                                PictureId = 2,
                                BackgroundId = 2,
                                Address = addresses.Find(a => a.UserId == 2),
                                Education = educations.Where(a => a.UserId == 2).ToList(),
                                Hobbies = hobbies.Where(a => a.UserId == 2).ToList()
                            },
                            new User
                            {
                                UserId = 3,
                                FirstName = "Avel",
                                LastName = "Magpantay",
                                UserName = "avma",
                                Password = "testtest1",
                                EmailAddress = "avel.magpantay@gmail.com",
                                BirthDate = DateTime.Now.AddYears(-25),
                                PictureId = 3,
                                BackgroundId = 3,
                                Address = addresses.Find(a => a.UserId == 3),
                                Education = educations.Where(a => a.UserId == 3).ToList(),
                                Hobbies = hobbies.Where(a => a.UserId == 3).ToList()
                            }
                        };

            return users;
        }
    }
}
