using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _1311.Models.Repository.ICommentRepositorys
{
    public class CommentRepository : ICommentRepositorys<Comment>
    {
        private readonly AppDbContext context;

        public CommentRepository(AppDbContext context)
        {
            this.context = context;
        }

        public void Add(Comment entity, string userid, int colisid)
        {
            int count = this.context.Comment.Count();
            int id;
            if (count == 0) { id = 1; }
            else { id = this.context.Comment.Max(colisa => colisa.Id) + 1; }
            string nom = "Comment " + id;

            entity.Name = nom;
            entity.DateCreation = DateTime.Now;
            entity.UserId = userid;
            entity.ColisId= colisid;
            this.context.Comment.Add(entity);
            this.context.SaveChanges();
        }

        public List<Comment> Get(int id)
        {
            List<Comment> list =this.context.Comment
                .Include(c=>c.Colis)
                .Include(c=>c.User)
                .Where(c =>c.ColisId==id)
                .OrderByDescending(p => p.DateCreation)
                .ToList();
            return list;
        }
    }
}
