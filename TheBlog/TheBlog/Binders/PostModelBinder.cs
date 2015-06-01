using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Autofac.Integration.Mvc;
using TheBlog.DAL.Interfaces;
using TheBlog.Model;

namespace TheBlog.Binders
{
    [ModelBinderType(typeof(Post))]
    public class PostModelBinder : DefaultModelBinder, IModelBinder
    {
        private readonly IBlogContext _context;

        public PostModelBinder(IBlogContext context)
        {
            _context = context;
        }

        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var post = (Post)base.BindModel(controllerContext, bindingContext);

            if (post.Category != null)
                post.Category = _context.Categories.FirstOrDefault(x => x.CategoryId == post.Category.CategoryId);

            var tags = bindingContext.ValueProvider.GetValue("Tags").AttemptedValue.Split(',');
            if (tags.Length > 0)
            {
                post.Tags = new List<Tag>();

                foreach (var tag in tags)
                {
                    var tagId = Convert.ToInt32(tag.Trim());
                    post.Tags.Add(_context.Tags.FirstOrDefault(x => x.Id == tagId));
                }
            }
            post.AddedOn = DateTime.Now;

            if (post.User == null)
            {
                post.User = _context.Users.FirstOrDefault();
                post.UserId = post.User.UserId;
            }

            return post;
        }
    }
}