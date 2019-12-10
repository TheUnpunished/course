using Authentification.Models;
using Authentification.Repositories.Entities;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentification.Repositories.ProjectRepository
{
    public class CategoryRepository : IRepository<Category>
    {
        private readonly string SQL_SELECT_ALL = "SELECT category.id, category.name, people.id, people.user, people.name, people.surname FROM public.category join people on category.people_id = people.id order by category.id;";
        private readonly string SQL_SELECT = "SELECT category.id, category.name, people.id, people.user, people.name, people.surname FROM public.category join people on category.people_id = people.id where category.id=@0;";
        private readonly string SQL_UPDATE = "UPDATE public.category SET name=@1, people_id=@2 WHERE id = @0;";
        private readonly string SQL_DELETE = "DELETE FROM public.category WHERE id = @0;";
        private readonly string SQL_INSERT = "INSERT INTO public.category(name, people_id) VALUES (@1, @2);";
        private readonly string SQL_INSERT_ID = "INSERT INTO public.category(id, name, people_id) VALUES (@0, @1, @2);";

        private readonly string _connectionString;

        public CategoryRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Category> AddAsync(Category entity)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                using (var comm = conn.CreateCommand())
                {
                    comm.CommandText = SQL_INSERT;
                    comm.Parameters.AddWithValue("1", NpgsqlDbType.Text, entity.name);     
                    comm.Parameters.AddWithValue("2", NpgsqlDbType.Bigint, entity.peopleid);
                    using (var reader = await comm.ExecuteReaderAsync())
                    {
                        if (!await reader.ReadAsync()) return null;
                        return new Category
                        {
                            name = reader.GetString(1),
                            peopleid = reader.GetInt64(2),
                        };
                    }
                }
            }
        }

        public async Task<Category> EditAsync(Category entity)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                using (var comm = conn.CreateCommand())
                {
                    comm.CommandText = SQL_UPDATE;
                    comm.Parameters.AddWithValue("0", NpgsqlDbType.Bigint, entity.Id);
                    comm.Parameters.AddWithValue("1", NpgsqlDbType.Text, entity.name);
                    comm.Parameters.AddWithValue("2", NpgsqlDbType.Text, entity.peopleid);
                    using (var reader = await comm.ExecuteReaderAsync())
                    {
                        if (!await reader.ReadAsync()) return null;
                        return new Category
                        {
                            Id = reader.GetInt64(0),
                            name = reader.GetString(1),
                            peopleid = reader.GetInt64(2)
                        };
                    }
                }
            }
        }

        public async Task<Category> AddId(Category entity)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                using (var comm = conn.CreateCommand())
                {
                    comm.CommandText = SQL_INSERT_ID;
                    comm.Parameters.AddWithValue("0", NpgsqlDbType.Bigint, entity.Id);
                    comm.Parameters.AddWithValue("1", NpgsqlDbType.Text, entity.name);
                    comm.Parameters.AddWithValue("2", NpgsqlDbType.Bigint, entity.peopleid);
                    using (var reader = await comm.ExecuteReaderAsync())
                    {
                        if (!await reader.ReadAsync()) return null;
                        return new Category
                        {
                            Id = reader.GetInt64(0),
                            name = reader.GetString(1),
                            peopleid = reader.GetInt64(2),
                        };
                    }
                }
            }
        }

        public async Task<Category[]> GetAllAsync()
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                using (var comm = conn.CreateCommand())
                {
                    var result = new List<Category>();
                    comm.CommandText = SQL_SELECT_ALL;
                    using (var reader = await comm.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            result.Add(new Category
                            {
                                Id = reader.GetInt64(0),
                                name = reader.GetString(1),
                                people_id = new People
                                {
                                    Id = reader.GetInt64(2),
                                    user = reader.GetString(3),
                                    name = reader.GetString(4),
                                    surname = reader.GetString(5)
                                }
                            });
                        }
                        return result.ToArray();
                    }
                }
            }
        }

        public async Task<Category> GetAsync(long id)
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
                        return new Category
                        {
                            Id = reader.GetInt64(0),
                            name = reader.GetString(1),
                            people_id = new People
                            {
                                Id = reader.GetInt64(2),
                                user = reader.GetString(3),
                                name = reader.GetString(4),
                                surname = reader.GetString(5)
                            }
                        };
                    }
                }
            }
        }

        public async Task<Category> RemoveAsync(long id)
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
                        return new Category
                        {
                            Id = reader.GetInt64(0)
                        };
                    }
                }
            }
        }
    }
}
