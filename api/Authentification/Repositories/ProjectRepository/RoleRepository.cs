using Authentification.Models;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentification.Repositories.ProjectRepository
{
    public class RoleRepository : IRepository<Role>
    {
        private readonly string SQL_SELECT_ALL = "SELECT user_id, status FROM public.role join users on role.user_id = users.id order by user_id;";
        private readonly string SQL_SELECT = "SELECT user_id, status FROM public.role join users on role.user_id = users.id where user_id=@0;";
        private readonly string SQL_UPDATE = "UPDATE public.role SET status=@1 WHERE user_id = @0;";
        private readonly string SQL_DELETE = "DELETE FROM public.role WHERE user_id = @0;";
        private readonly string SQL_INSERT = "INSERT INTO public.role(user_id, status) VALUES (@0, @1);";

        private readonly string _connectionString;

        public RoleRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Role> AddAsync(Role entity)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                using (var comm = conn.CreateCommand())
                {
                    comm.CommandText = SQL_INSERT;
                    comm.Parameters.AddWithValue("0", NpgsqlDbType.Bigint, entity.Id);
                    comm.Parameters.AddWithValue("1", NpgsqlDbType.Text, entity.status);
                    using (var reader = await comm.ExecuteReaderAsync())
                    {
                        if (!await reader.ReadAsync()) return null;
                        return new Role
                        {
                            Id = reader.GetInt64(0),
                            status = reader.GetString(1)
                        };
                    }
                }
            }
        }

        public async Task<Role> EditAsync(Role entity)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                using (var comm = conn.CreateCommand())
                {
                    comm.CommandText = SQL_UPDATE;
                    comm.Parameters.AddWithValue("0", NpgsqlDbType.Bigint, entity.Id);
                    comm.Parameters.AddWithValue("1", NpgsqlDbType.Text, entity.status);
                    using (var reader = await comm.ExecuteReaderAsync())
                    {
                        if (!await reader.ReadAsync()) return null;
                        return new Role
                        {
                            Id = reader.GetInt64(0),
                            status = reader.GetString(1)
                        };
                    }
                }
            }
        }

        public async Task<Role[]> GetAllAsync()
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                using (var comm = conn.CreateCommand())
                {
                    var result = new List<Role>();
                    comm.CommandText = SQL_SELECT_ALL;
                    using (var reader = await comm.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            result.Add(new Role
                            {
                                Id = reader.GetInt64(0),
                                status = reader.GetString(1)
                            });
                        }
                        return result.ToArray();
                    }
                }
            }
        }

        public async Task<Role> GetAsync(long id)
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
                        return new Role
                        {
                            Id = reader.GetInt64(0),
                            status = reader.GetString(1)
                        };
                    }
                }
            }
        }

        public async Task<Role> RemoveAsync(long id)
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
                        return new Role
                        {
                            Id = reader.GetInt64(0)
                        };
                    }
                }
            }
        }

        public Task<Role> AddId(Role entity)
        {
            return null;
        }
    }
}
