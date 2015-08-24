using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniRitter.UniRitter2015.Specs.Models
{
    public class Post : IModel
    {
        public Guid? id { get; set; }

        public string body { get; set; }

        public string title { get; set; }

        public Guid authorId { get; set; }

        public IEnumerable<string> tags { get; set; }

        public bool Equals(Post other)
        {
            if (other == null) return false;

            return
                id == other.id
                && body == other.body
                && title == other.title
                && authorId == other.authorId;
        }

        public override bool Equals(object obj)
        {
            if (obj != null)
            {
                return Equals(obj as Post);
            }
            return false;
        }

        bool IModel.IsEquals(IModel other)
        {
            if (other == null ||
                other.GetType() != typeof(Post)) return false;

            var post = (Post)other;

            return
                id == post.id
                && body == post.body
                && title == post.title
                && authorId == post.authorId;
        }

        bool IModel.AttributeEquals(IModel other)
        {
            if (other == null ||
                other.GetType() != typeof(Post)) return false;
            var post = (Post)other;

            return
                body == post.body
                && title == post.title
                && authorId == post.authorId;
        }

        Guid? IModel.GetId()
        {
            return id;
        }

        public override string ToString()
        {
            return
                string.Format("{0} - {1} - {2} - {3} - {4} tags",
                    (id == null) ? "?" : this.id.Value.ToString(),
                    this.title,
                    this.body,
                    this.authorId,
                    (tags == null) ? 0 : tags.Count());
        }
    }
}