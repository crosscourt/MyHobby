using MyHobby.ApiModels;
using MyHobby.Models;
using MyHobby.Repositories;
using MyHobby.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyHobby.Controllers
{
    [TokenAuthentication]
    public class BusinessesController : BaseApiController
    {
        private BusinessRepository _businessRepository;

        public BusinessesController(BusinessRepository businessRepository)
        {
            _businessRepository = businessRepository;
        }

        // GET api/<controller>
        public IEnumerable<BusinessDTO> Get()
        {
            var businesses = _businessRepository.GetAllBusinesses();
            return businesses.ToList().ConvertAll(b => new BusinessDTO(b, UserId));
        }

        // GET api/<controller>/5        
        public BusinessDTO Get(int id)
        {
            var business = _businessRepository.GetBusiness(id);
            return new BusinessDTO(business, UserId);
        }

        [Route("api/businesses/{businessId}/reviews")]
        public IEnumerable<BusinessReviewDTO> GetReviewsByBusinessId(int businessId, string rating = null)
        {
            var reviews = _businessRepository.GetReviews(businessId, rating);
            var businessReviews = reviews.ToList().ConvertAll(r => new BusinessReviewDTO(r));
            return businessReviews;
        }
        
        [HttpPost]
        [Route("api/businesses/{businessId}/reviews")]
        [RequireLoginAttribute_old]
        public Result<BusinessReview> AddReview(int businessId, BusinessReviewDTO dto)
        {
            //todo: check has enrolled/attended class

            BusinessReview review = new BusinessReview() { Comment = dto.Comment, CreatedTime = DateTimeOffset.UtcNow, CreatedById = UserId, BusinessId = businessId };
            
            review.Images = new List<BusinessReviewImage>();
            foreach (BusinessReviewImage image in dto.Images)
            {
                review.Images.Add(image);
            }

            // must add after adding images
            _businessRepository.AddReview(review);

            Result<BusinessReview> res = new Result<BusinessReview>(true, review);
            return res;
        }

        [Route("api/businesses/{businessId}/users")]
        public IEnumerable<BusinessUserDTO> GetUsers(int businessId)
        {
            var staffs = _businessRepository.GetUsers(businessId);
            var staffDTOs = staffs.ToList().ConvertAll(s => new BusinessUserDTO(s));
            return staffDTOs;
        }

        [Route("api/businesses/{businessId}/users")]
        [HttpPost]
        [RequireLoginAttribute_old]
        public Result AddUser(int businessId, BusinessUserDTO businessUser)
        {
            Business business = _businessRepository.GetBusiness(businessId);
            if (business.IsStaffInRole(UserId, UserRole.Admin) || IsSuperUser)
            {
                if (!business.BusinessUsers.Any(bu => bu.UserId == businessUser.User.Id))
                {
                    _businessRepository.AddUser(businessId, businessUser);
                    Result result = new Result(true);
                    return result;
                }

                return new Result(false) { Message = "already added" };
            }

            return new Result(false) { Message = "not admin" };
        }

        [Route("api/businesses/{businessId}/users")]
        [HttpPut]
        [RequireLoginAttribute_old]
        public Result UpdateUser(int businessId, BusinessUserDTO businessUser)
        {
            Business business = _businessRepository.GetBusiness(businessId);
            if (business.IsStaffInRole(UserId, UserRole.Admin) || IsSuperUser)
            {                
                _businessRepository.UpdateUser(businessId, businessUser);
                Result result = new Result(true);
                return result;                
            }

            return new Result(false) { Message = "not admin" };
        }

        [Route("api/businesses/{businessId}/users")]
        [HttpDelete]
        [RequireLoginAttribute_old]
        public Result DeleteUser(int businessId, [FromBody]int userId)
        {
            _businessRepository.DeleteUser(businessId, userId);
            Result result = new Result(true);
            return result;
        }
        
        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}