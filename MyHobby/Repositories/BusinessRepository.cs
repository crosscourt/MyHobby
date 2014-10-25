using MyHobby.ApiModels;
using MyHobby.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace MyHobby.Repositories
{
    public class BusinessRepository
    {
        private MyHobbyContext _ctx;

        public BusinessRepository(MyHobbyContext ctx)
        {
            _ctx = ctx;
        }

        public Business GetBusiness(int id)
        {
            return _ctx.Businesses.Include("HobbyCategories").Include("Suburb").Include("BusinessUsers").SingleOrDefault(b => b.Id == id);
        }

        public IQueryable<Business> GetAllBusinesses()
        {
            return _ctx.Businesses.Include("HobbyCategories").Include("Suburb");
        }

        public IQueryable<BusinessReview> GetReviews(int businessId, string rating)
        {
            var reviews = _ctx.BusinessReviews.Include("Images").Include("Comments").Include("Comments.CreatedBy").Where(br => br.BusinessId == businessId);
            
            if (rating != null)
            {
                if (rating == "good")
                {
                    reviews = reviews.Where(r => r.Rating >= 3);
                }
                else if (rating == "bad")
                {
                    reviews = reviews.Where(r => r.Rating <= 2);
                }
            }

            return reviews;
        }

        public void AddReview(BusinessReview review)
        {
            _ctx.BusinessReviews.Add(review);
            _ctx.SaveChanges();
        }

        public IQueryable<BusinessUser> GetUsers(int businessId)
        {
            var businessUsers = _ctx.BusinessUsers.Include("User").Where(bu => bu.BusinessId == businessId);
            return businessUsers;
        }
        
        public void AddUser(int businessId, BusinessUserDTO dto)
        {
            //Business business = _ctx.Businesses.Include("BusinessUsers").SingleOrDefault(b => b.Id == businessId);
            Business business = _ctx.Businesses.SingleOrDefault(b => b.Id == businessId);
            User user = _ctx.Users.Find(dto.User.Id);

            if (business != null && user != null)
            {
                BusinessUser businessUser = new BusinessUser() { Business = business, User = user, Role = dto.Role, Title = dto.Title };

                _ctx.BusinessUsers.Add(businessUser);
                _ctx.SaveChanges();
            }
        }

        public void UpdateUser(int businessId, BusinessUserDTO dto)
        {
            BusinessUser businessUser = _ctx.BusinessUsers.SingleOrDefault(b => b.BusinessId == businessId && b.UserId == dto.User.Id);
            if (businessUser != null)
            {
                businessUser.Role = dto.Role;
                businessUser.Title = dto.Title;

                _ctx.SaveChanges();
            }
        }

        public void DeleteUser(int businessId, int userId)
        {
            BusinessUser businessUser = _ctx.BusinessUsers.Find(businessId, userId);

            if (businessUser != null)
            {
                _ctx.BusinessUsers.Remove(businessUser);
                _ctx.SaveChanges();
            }
        }        
    }
}