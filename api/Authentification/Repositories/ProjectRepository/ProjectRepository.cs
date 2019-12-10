using Authentification.Models;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentification.Repositories.Entities
{
    public class ProjectRepository : IRepository<Project>
    {
        private readonly string SQL_SELECT_ALL = "SELECT project.id, project.name, project.description, project.public, users.id, repository FROM public.project join users on project.user_id = users.id order by project.id;";
        private readonly string SQL_SELECT = "SELECT project.id, project.name, project.description, project.public, users.id, repository FROM public.project join users on project.user_id = users.id  where project.id=@0;";
        private readonly string SQL_UPDATE = "UPDATE public.project SET name=@1, description=@2, public=@3, user_id=@4, repository=@5 WHERE id = @0;";
        private readonly string SQL_DELETE = "DELETE FROM public.project WHERE id = @0;";
        private readonly string SQL_INSERT = "INSERT INTO public.project(name, description, public, user_id, repository) VALUES (@1, @2, @3, @4, @5);";
        private readonly string SQL_INSERT_ID = "INSERT INTO public.project(id, name, description, public, user_id, repository) VALUES (@0, @1, @2, @3, @4, @5);";
        private readonly string SQL_SELECT_NAME = "SELECT repository FROM public.project where project.id=@0;";

        private readonly string _connectionString;

        public ProjectRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Project> AddAsync(Project entity)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                using (var comm = conn.CreateCommand())
                {
                    comm.CommandText = SQL_INSERT;
                    comm.Parameters.AddWithValue("1", NpgsqlDbType.Text, entity.name);
                    comm.Parameters.AddWithValue("2", NpgsqlDbType.Text, entity.description);
                    comm.Parameters.AddWithValue("3", NpgsqlDbType.Boolean, entity.isPublic);
                    comm.Parameters.AddWithValue("4", NpgsqlDbType.Bigint, entity.userid);
                    comm.Parameters.AddWithValue("5", NpgsqlDbType.Text, entity.repository);
                    using (var reader = await comm.ExecuteReaderAsync())
                    {
                        if (!await reader.ReadAsync()) return null;
                        return new Project
                        {
                            name = reader.GetString(1),
                            description = reader.GetString(2),
                            isPublic = reader.GetBoolean(3),
                            userid = reader.GetInt64(4),
                            repository = reader.GetString(5)
                        };
                    }
                }
            }
        }

        public async Task<Project> EditAsync(Project entity)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                using (var comm = conn.CreateCommand())
                {
                    comm.CommandText = SQL_UPDATE;
                    comm.Parameters.AddWithValue("0", NpgsqlDbType.Bigint, entity.Id);
                    comm.Parameters.AddWithValue("1", NpgsqlDbType.Text, entity.name);
                    comm.Parameters.AddWithValue("2", NpgsqlDbType.Text, entity.description);
                    comm.Parameters.AddWithValue("3", NpgsqlDbType.Boolean, entity.isPublic);
                    comm.Parameters.AddWithValue("4", NpgsqlDbType.Bigint, entity.userid);
                    comm.Parameters.AddWithValue("5", NpgsqlDbType.Text, entity.repository);
                    using (var reader = await comm.ExecuteReaderAsync())
                    {
                        if (!await reader.ReadAsync()) return null;
                        return new Project
                        {
                            Id = reader.GetInt64(0),
                            name = reader.GetString(1),
                            description = reader.GetString(2),
                            isPublic = reader.GetBoolean(3),
                            userid = reader.GetInt64(4),
                            repository = reader.GetString(5)
                        };
                    }
                }
            }
        }

        public async Task<Project[]> GetAllAsync()
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                using (var comm = conn.CreateCommand())
                {
                    var result = new List<Project>();
                    comm.CommandText = SQL_SELECT_ALL;
                    using (var reader = await comm.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            result.Add(new Project
                            {
                                Id = reader.GetInt64(0),
                                name = reader.GetString(1),
                                description = reader.GetString(2),
                                isPublic = reader.GetBoolean(3),
                                userid = reader.GetInt64(4),
                                repository = reader.GetString(5)
                            });
                        }
                        return result.ToArray();
                    }
                }
            }
        }

        public async Task<Project> GetAsync(long id)
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
                        return new Project
                        {
                            Id = reader.GetInt64(0),
                            name = reader.GetString(1),
                            description = reader.GetString(2),
                            isPublic = reader.GetBoolean(3),
                            userid = reader.GetInt64(4),
                            repository = reader.GetString(5)
                        };
                    }
                }
            }
        }

        public async Task<Project> GetNameAsync(long id)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                using (var comm = conn.CreateCommand())
                {
                    comm.CommandText = SQL_SELECT_NAME;
                    comm.Parameters.AddWithValue("0", NpgsqlDbType.Bigint, id);
                    using (var reader = await comm.ExecuteReaderAsync())
                    {
                        if (!await reader.ReadAsync()) return null;
                        return new Project
                        {
                            
                            repository = reader.GetString(0)
                        };
                    }
                }
            }
        }

        public async Task<Project> RemoveAsync(long id)
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
                        return new Project
                        {
                            Id = reader.GetInt64(0)
                        };
                    }
                }
            }
        }

        public async Task<Project> AddId(Project entity)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                using (var comm = conn.CreateCommand())
                {
                    comm.CommandText = SQL_INSERT_ID;
                    comm.Parameters.AddWithValue("0", NpgsqlDbType.Bigint, entity.Id);
                    comm.Parameters.AddWithValue("1", NpgsqlDbType.Text, entity.name);
                    comm.Parameters.AddWithValue("2", NpgsqlDbType.Text, entity.description);
                    comm.Parameters.AddWithValue("3", NpgsqlDbType.Boolean, entity.isPublic);
                    comm.Parameters.AddWithValue("4", NpgsqlDbType.Bigint, entity.userid);
                    comm.Parameters.AddWithValue("5", NpgsqlDbType.Text, entity.repository);
                    using (var reader = await comm.ExecuteReaderAsync())
                    {
                        if (!await reader.ReadAsync()) return null;
                        return new Project
                        {
                            Id = reader.GetInt64(0),
                            name = reader.GetString(1),
                            description = reader.GetString(2),
                            isPublic = reader.GetBoolean(3),
                            userid = reader.GetInt64(4),
                            repository = reader.GetString(5)
                        };
                    }
                }
            }
        }
    }
}
