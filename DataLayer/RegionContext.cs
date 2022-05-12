using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BusinessLayer;
using Microsoft.EntityFrameworkCore;

namespace DataLayer
{
    public class RegionContext : IDB<Region, int>
    {
        private IzpitDBContext _context;

        public RegionContext(IzpitDBContext context)
        {
            _context = context;
        }

        public void Create(Region item)
        {
            try
            {
                _context.Regions.Add(item);
                _context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Region Read(int key, bool noTracking = false, bool useNavigationProperties = false)
        {
            try
            {
                IQueryable<Region> query = _context.Regions;
                if (noTracking)
                {
                    query = query.AsNoTrackingWithIdentityResolution();
                }

                if (useNavigationProperties)
                {
                    query = query.Include(r => r.Users);
                }

                return query.SingleOrDefault(r => r.ID == key);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<Region> Read(int skip, int take, bool useNavigationProperties = false)
        {
            try
            {
                IQueryable<Region> query = _context.Regions.AsNoTrackingWithIdentityResolution();

                if (useNavigationProperties)
                {
                    query = query.Include(r => r.Users);
                }

                return query.Skip(skip).Take(take).ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<Region> ReadAll(bool useNavigationProperties = false)
        {
            try
            {
                IQueryable<Region> query = _context.Regions.AsNoTrackingWithIdentityResolution();

                if (useNavigationProperties)
                {
                    query = query.Include(r => r.Users);
                }

                return query.ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Update(Region item, bool useNavigationProperties = false)
        {
            try
            {
                Region regionFromDB = Read(item.ID, useNavigationProperties);

                if (useNavigationProperties)
                {
                    List<User> users = new List<User>();

                    foreach (User user in item.Users)
                    {
                        if (_context.Users.Find(user.ID) != null)
                        {
                            users.Add(_context.Users.Find(user.ID));
                        }
                        else
                        {
                            users.Add(user);
                        }
                    }
                    item.Users = users;
                }

                _context.Entry(regionFromDB).CurrentValues.SetValues(item);
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
                _context.Regions.Remove(Read(key));
                _context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        
    }
}
