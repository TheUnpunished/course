using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Authentification.Models;
using Npgsql;
using NpgsqlTypes;

namespace Authentification.Repositories.UserRepository
{
    public class UserRepository : IRepository<User>
    {
        private readonly string _connectionString;

        public UserRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        private readonly string SQL_SELECT_ALL = "SELECT id, email, password FROM public.users order by id;";
        private readonly string SQL_SELECT = "SELECT id, email, password FROM public.users where id = @0;";
        private readonly string SQL_SELECT_EMAIL = "SELECT id, email, password FROM public.users where email = @0 order by id;";
        private readonly string SQL_UPDATE = "UPDATE public.users SET email = @1, password = @2 WHERE id = @0;";
        private readonly string SQL_DELETE = "DELETE FROM public.users WHERE id = @0;";
        private readonly string SQL_INSERT = "INSERT INTO public.users(email, password) VALUES(@1, @2);";

        public async Task<User> GetAsync(long id)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                using (var comm = conn.CreateCommand())
                {
                    comm.CommandText = SQL_SELECT;
                    comm.Parameters.AddWithValue("0", NpgsqlDbType.Bigint, id);
                    using (var reader = await comm.ExecuteReaderAsync())
                    {
                        if (!await reader.ReadAsync()) return null;
                        return new User
                        {
                            Id = reader.GetInt64(0),
                            email = reader.GetString(1),
                            password = reader.GetString(2)
                        };
                    }
                }
            }
        }

        public async Task<User> GetAsyncEmail(string email)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                using (var comm = conn.CreateCommand())
                {
                    comm.CommandText = SQL_SELECT_EMAIL;
                    comm.Parameters.AddWithValue("0", NpgsqlDbType.Text, email);
                    using (var reader = await comm.ExecuteReaderAsync())
                    {
                        if (!await reader.ReadAsync()) return null;
                        return new User
                        {
                            Id = reader.GetInt64(0),
                            email = reader.GetString(1),
                            password = reader.GetString(2)
                        };
                    }
                }
            }
        }

        public async Task<User[]> GetAllAsync()
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                using (var comm = conn.CreateCommand())
                {
                    var result = new List<User>();
                    comm.CommandText = SQL_SELECT_ALL;
                    using (var reader = await comm.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            result.Add(new User
                            {
                                Id = reader.GetInt64(0),
                                email = reader.GetString(1),
                                password = reader.GetString(2)
                            });
                        }
                        return result.ToArray();
                    }
                }
            }
        }

        public async Task<User> AddAsync(User entity)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                using (var comm = conn.CreateCommand())
                {
                    comm.CommandText = SQL_INSERT;
                    comm.Parameters.AddWithValue("1", NpgsqlDbType.Text, entity.email);
                    comm.Parameters.AddWithValue("2", NpgsqlDbType.Text, entity.password);
                    using (var reader = await comm.ExecuteReaderAsync())
                    {
                        if (!await reader.ReadAsync()) return null;
                        return new User
                        {
                            email = reader.GetString(1),
                            password = reader.GetString(2),
                        };
                    }
                }
            }
        }

        public async Task<User> EditAsync(User entity)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                using (var comm = conn.CreateCommand())
                {
                    comm.CommandText = SQL_UPDATE;
                    comm.Parameters.AddWithValue("0", NpgsqlDbType.Bigint, entity.Id);
                    comm.Parameters.AddWithValue("1", NpgsqlDbType.Text, entity.email);
                    comm.Parameters.AddWithValue("2", NpgsqlDbType.Text, entity.password);
                    using (var reader = await comm.ExecuteReaderAsync())
                    {
                        if (!await reader.ReadAsync()) return null;
                        return new User
                        {
                            Id = reader.GetInt64(0),
                            email = reader.GetString(1),
                            password = reader.GetString(2)
                        };
                    }
                }
            }
        }

        public async Task<User> RemoveAsync(long id)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                using (var comm = conn.CreateCommand())
                {
                    comm.CommandText = SQL_DELETE;
                    comm.Parameters.AddWithValue("0", NpgsqlDbType.Bigint, id);
                    using (var reader = await comm.ExecuteReaderAsync())
                    {
                        if (!await reader.ReadAsync()) return null;
                        return new User
                        {
                            Id = reader.GetInt64(0)
                        };
                    }
                }
            }
        }



        public bool isUserExists(User user)
        {
            var conn = new NpgsqlConnection(_connectionString);
            conn.Open();
            var comm = conn.CreateCommand();
            comm.CommandText = "SELECT * FROM public.\"users\" WHERE \"email\"='" + user.email + "'";
            var reader = comm.ExecuteReader();
            if (reader.Read())
            {
                return true;
            }
            return false;
        }
        public bool userAuthenticate(User user)
        {
            var conn = new NpgsqlConnection(_connectionString);
            conn.Open();
            var comm = conn.CreateCommand();
            comm.CommandText = "SELECT * FROM users WHERE \"email\"='" + user.email + "' AND \"password\"='" + user.password + "'";
            var reader = comm.ExecuteReader();
            if (reader.Read())
            {
                return true;
            }
            return false;
        }

        public Task<User> AddId(User entity)
        {
            throw new NotImplementedException();
        }
    }
}
