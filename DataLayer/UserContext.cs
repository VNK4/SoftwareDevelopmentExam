using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer;
using Microsoft.EntityFrameworkCore;

namespace DataLayer
{
    public class UserContext : IDB<User, int>
    {
        private IzpitDBContext _context;

        public UserContext(IzpitDBContext context)
        {
            _context = context;
        }

        public void Create(User item)
        {
            try
            {
                _context.Users.Add(item);
                _context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public User Read(int key, bool noTracking = false, bool useNavigationProperties = false)
        {
            try
            {
                IQueryable<User> query = _context.Users;

                if (noTracking)
                {
                    query = query.AsNoTrackingWithIdentityResolution();
                }

                if (useNavigationProperties)
                {
                    query = query.Include(u => u.Friends).Include(u => u.Interests);
                }

                return query.SingleOrDefault(u => u.ID == key);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<User> ReadAll(bool useNavigationProperties = false)
        {
            try
            {
                IQueryable<User> query = _context.Users.AsNoTracking();

                if (useNavigationProperties)
                {
                    query = query.Include(u => u.Friends).Include(u => u.Interests);
                }

                return query.ToList();
            }

            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<User> Read(int skip, int take, bool useNavigationProperties)
        {
            try
            {
                IQueryable<User> query = _context.Users.AsNoTrackingWithIdentityResolution();

                if (useNavigationProperties)
                {
                    query = query.Include(u => u.Friends).Include(u => u.Interests);
                }

                return query.Skip(skip).Take(take).ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Update(User item, bool useNavigationProperties = false)
        {
            try
            {
                User userFromDB = Read(item.ID, useNavigationProperties);

                if (useNavigationProperties)
                {
                    List<User> friends = new List<User>();

                    foreach (var friend in item.Friends)
                    {
                        User friendFromDB = _context.Users.Find(friend.ID);

                        if (friendFromDB != null)
                        {
                            friends.Add(friendFromDB);
                        }
                        else
                        {
                            friends.Add(friend);
                        }
                    }

                    List<Interest> interests = new List<Interest>();

                    foreach (var interest in item.Interests)
                    {
                        Interest interestFromDB = _context.Interests.Find(interest.ID);

                        if (interestFromDB != null)
                        {
                            interests.Add(interestFromDB);
                        }
                        else
                        {
                            interests.Add(interest);
                        }
                    }

                    item.Friends = friends;
                    item.Interests = interests;

                }

                _context.Entry(userFromDB).CurrentValues.SetValues(item);
                _context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Delete(int key)
        {
            try
            {
                _context.Users.Remove(Read(key));
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
