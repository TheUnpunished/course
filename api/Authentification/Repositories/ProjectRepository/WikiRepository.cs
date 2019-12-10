using Authentification.Models;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentification.Repositories.ProjectRepository
{
    public class WikiRepository : IRepository<Wiki>
    {
        private readonly string SQL_SELECT_ALL = "SELECT id, name, description, file_path FROM public.wiki order by id;";
        private readonly string SQL_SELECT = "SELECT id, name, description, file_path FROM public.wiki where wiki.id=@0;";
        private readonly string SQL_UPDATE = "UPDATE public.wiki SET name=@1, description=@2, file_path=@3 WHERE id = @0;";
        private readonly string SQL_DELETE = "DELETE FROM public.wiki WHERE id = @0;";
        private readonly string SQL_INSERT = "INSERT INTO public.wiki(name, description, file_path) VALUES (@1, @2, @3);";
        private readonly string SQL_INSERT_ID = "INSERT INTO public.wiki(id, name, description, file_path) VALUES (@0, @1, @2, @3);";

        private readonly string _connectionString;

        public WikiRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Wiki> AddAsync(Wiki entity)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                using (var comm = conn.CreateCommand())
                {
                    comm.CommandText = SQL_INSERT;
                    comm.Parameters.AddWithValue("1", NpgsqlDbType.Text, entity.name);
                    comm.Parameters.AddWithValue("2", NpgsqlDbType.Text, entity.description);
                    comm.Parameters.AddWithValue("3", NpgsqlDbType.Text, entity.file_path);
                    using (var reader = await comm.ExecuteReaderAsync())
                    {
                        if (!await reader.ReadAsync()) return null;
                        return new Wiki
                        {
                            name = reader.GetString(1),
                            description = reader.GetString(2),
                            file_path = reader.GetString(3),
                        };
                    }
                }
            }
        }

        public async Task<Wiki> EditAsync(Wiki entity)
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
                    comm.Parameters.AddWithValue("3", NpgsqlDbType.Text, entity.file_path);
                    using (var reader = await comm.ExecuteReaderAsync())
                    {
                        if (!await reader.ReadAsync()) return null;
                        return new Wiki
                        {
                            Id = reader.GetInt64(0),
                            name = reader.GetString(1),
                            description = reader.GetString(2),
                            file_path = reader.GetString(3),
                        };
                    }
                }
            }
        }

        public async Task<Wiki[]> GetAllAsync()
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                using (var comm = conn.CreateCommand())
                {
                    var result = new List<Wiki>();
                    comm.CommandText = SQL_SELECT_ALL;
                    using (var reader = await comm.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            result.Add(new Wiki
                            {
                                Id = reader.GetInt64(0),
                                name = reader.GetString(1),
                                description = reader.GetString(2),
                                file_path = reader.GetString(3),
                            });
                        }
                        return result.ToArray();
                    }
                }
            }
        }

        public async Task<Wiki> GetAsync(long id)
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
                        return new Wiki
                        {
                            Id = reader.GetInt64(0),
                            name = reader.GetString(1),
                            description = reader.GetString(2),
                            file_path = reader.GetString(3),
                        };
                    }
                }
            }
        }

        public async Task<Wiki> RemoveAsync(long id)
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
                        return new Wiki
                        {
                            Id = reader.GetInt64(0)
                        };
                    }
                }
            }
        }

        public async Task<Wiki> AddId(Wiki entity)
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
                    comm.Parameters.AddWithValue("3", NpgsqlDbType.Text, entity.file_path);
                    using (var reader = await comm.ExecuteReaderAsync())
                    {
                        if (!await reader.ReadAsync()) return null;
                        return new Wiki
                        {
                            Id = reader.GetInt64(0),
                            name = reader.GetString(1),
                            description = reader.GetString(2),
                            file_path = reader.GetString(3),
                        };
                    }
                }
            }
        }
    }
}
