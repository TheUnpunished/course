using Authentification.Models;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentification.Repositories.ProjectRepository
{
    public class SpringRepository : IRepository<Spring>
    {
        private readonly string SQL_SELECT_ALL = "SELECT spring.id, spring.name, spring.description, spring.status, wiki.id, wiki.name, spring.date FROM public.spring join public.wiki on spring.wiki = wiki.id order by spring.id;";
        private readonly string SQL_SELECT = "SELECT spring.id, spring.name, spring.description, spring.status, wiki.id, wiki.name, spring.date FROM public.spring join public.wiki on spring.wiki = wiki.id where spring.id=@0;";
        private readonly string SQL_UPDATE = "UPDATE public.spring SET name=@1, description=@2, status=@3, wiki=@4, date=@5 WHERE id = @0;";
        private readonly string SQL_DELETE = "DELETE FROM public.spring WHERE id = @0;";
        private readonly string SQL_INSERT = "INSERT INTO public.spring(name, description, status, wiki, date) VALUES (@1, @2, @3, @4, @5);";
        private readonly string SQL_INSERT_ID = "INSERT INTO public.spring(id, name, description, status, wiki, date) VALUES (@0, @1, @2, @3, @4, @5);";

        private readonly string _connectionString;

        public SpringRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Spring> AddAsync(Spring entity)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                using (var comm = conn.CreateCommand())
                {
                    comm.CommandText = SQL_INSERT;
                    comm.Parameters.AddWithValue("1", NpgsqlDbType.Text, entity.name);
                    comm.Parameters.AddWithValue("2", NpgsqlDbType.Text, entity.description);
                    comm.Parameters.AddWithValue("3", NpgsqlDbType.Text, entity.status);
                    comm.Parameters.AddWithValue("4", NpgsqlDbType.Bigint, entity.wikiid);
                    comm.Parameters.AddWithValue("5", NpgsqlDbType.TimestampTz, entity.date);
                    using (var reader = await comm.ExecuteReaderAsync())
                    {
                        if (!await reader.ReadAsync()) return null;
                        return new Spring
                        {
                            name = reader.GetString(1),
                            description = reader.GetString(2),
                            status = reader.GetString(3),
                            wikiid = reader.GetInt64(4),
                            date = reader.GetDateTime(5),
                        };
                    }
                }
            }
        }

        public async Task<Spring> EditAsync(Spring entity)
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
                    comm.Parameters.AddWithValue("3", NpgsqlDbType.Text, entity.status);
                    comm.Parameters.AddWithValue("4", NpgsqlDbType.Bigint, entity.wikiid);
                    comm.Parameters.AddWithValue("5", NpgsqlDbType.TimestampTz, entity.date);
                    using (var reader = await comm.ExecuteReaderAsync())
                    {
                        if (!await reader.ReadAsync()) return null;
                        return new Spring
                        {
                            Id = reader.GetInt64(0),
                            name = reader.GetString(1),
                            description = reader.GetString(2),
                            status = reader.GetString(3),
                            wikiid = reader.GetInt64(4),
                            date = reader.GetDateTime(5)
                        };
                    }
                }
            }
        }

        public async Task<Spring[]> GetAllAsync()
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                using (var comm = conn.CreateCommand())
                {
                    var result = new List<Spring>();
                    comm.CommandText = SQL_SELECT_ALL;
                    using (var reader = await comm.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            result.Add(new Spring
                            {
                                Id = reader.GetInt64(0),
                                name = reader.GetString(1),
                                description = reader.GetString(2),
                                status = reader.GetString(3),
                                wiki = new Wiki {
                                    Id = reader.GetInt64(4),
                                    name = reader.GetString(5)
                                },
                                date = reader.GetDateTime(6)
                            });
                        }
                        return result.ToArray();
                    }
                }
            }
        }

        public async Task<Spring> GetAsync(long id)
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
                        return new Spring
                        {
                            Id = reader.GetInt64(0),
                            name = reader.GetString(1),
                            description = reader.GetString(2),
                            status = reader.GetString(3),
                            wiki = new Wiki
                            {
                                Id = reader.GetInt64(4),
                                name = reader.GetString(5)
                            },
                            date = reader.GetDateTime(6)
                        };
                    }
                }
            }
        }

        public async Task<Spring> RemoveAsync(long id)
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
                        return new Spring
                        {
                            Id = reader.GetInt64(0)
                        };
                    }
                }
            }
        }

        public async Task<Spring> AddId(Spring entity)
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
                    comm.Parameters.AddWithValue("3", NpgsqlDbType.Text, entity.status);
                    comm.Parameters.AddWithValue("4", NpgsqlDbType.Bigint, entity.wikiid);
                    comm.Parameters.AddWithValue("5", NpgsqlDbType.TimestampTz, entity.date);
                    using (var reader = await comm.ExecuteReaderAsync())
                    {
                        if (!await reader.ReadAsync()) return null;
                        return new Spring
                        {
                            Id = reader.GetInt64(0),
                            name = reader.GetString(1),
                            description = reader.GetString(2),
                            status = reader.GetString(3),
                            wikiid = reader.GetInt64(4),
                            date = reader.GetDateTime(5),
                        };
                    }
                }
            }
        }
    }
}
