using IdentityManagementAPI.Data;
using IdentityManagementAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityManagementAPI.Repository
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly ApplicationDbContext _db;
        public SubjectRepository(ApplicationDbContext dbContext)
        {
            _db = dbContext;
        }
        public bool CreateSubject(Subject subject)
        {
            _db.Subjects.Add(subject);
            return Save();
        }

        public bool DeleteSubject(Subject subject)
        {
            _db.Subjects.Remove(subject);
            return Save();
        }

        public ICollection<Subject> GetSubjects()
        {

            return _db.Subjects.OrderBy(x => x.Name).ToList();
        }

        public Subject GetSubject(int subjectId)
        {
            return _db.Subjects.FirstOrDefault(x => x.Id == subjectId);
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool SubjectExists(string name)
        {
            return _db.Subjects.Any(x => x.Name.ToLower().Trim() == name.ToLower().Trim());
        }

        public bool SubjectExists(int id)
        {
            return _db.Subjects.Any(x => x.Id == id);
        }

        public bool UpdateSubject(Subject subject)
        {
            _db.Subjects.Update(subject);
            return Save();
        }
    }
}
