using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;

namespace CodeSamples.Patterns.Other
{
    #region Repository Pattern Classes and Interfaces
    interface IEntity
    {
        int Id { get; set; }
    }

    //can be solved with interface...
    interface IRepository<T> where T : IEntity
    {
        T GetById(int id);
        IEnumerable<T> GetAll();
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
        void Add(T entity);
        void Remove(T entity);
    }

    //...or obstract class
    class RepositoryAbstract<T> where T : IEntity
    {
        private readonly Collection<T> _entityRepository;

        public RepositoryAbstract(Collection<T> dbContext)
        {
            _entityRepository = dbContext;
        }

        public RepositoryAbstract() : this(new Collection<T>()) { }

        public T GetById(int id)
        {
            var result = _entityRepository.FirstOrDefault<T>(o => o.Id == id);
            return result;
        }

        public IEnumerable<T> List()
        {
            return _entityRepository;
        }

        public IEnumerable<T> List(Expression<Func<T, bool>> predicate)
        {
            return _entityRepository.AsQueryable().Where<T>(predicate).OrderBy(o => o.Id);
        }

        public void Add(T entity)
        {
            _entityRepository.Add(entity);
        }

        public void Delete(T entity)
        {
            _entityRepository.Remove(entity);
        }
    }

    class Entity : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    class EntityRepository : IRepository<Entity>
    {
        //_entityRepository is Database Context for this case
        private readonly Collection<Entity> _entityRepository = new Collection<Entity>();

        public void Add(Entity entity)
        {
            _entityRepository.Add(entity);
            //_entityRepository.SaveChanges();
        }

        public void Remove(Entity entity)
        {
            _entityRepository.Remove(entity);
        }

        public Entity GetById(int id)
        {
            var result = _entityRepository.FirstOrDefault(o => o.Id == id);
            return result;
        }

        public IEnumerable<Entity> GetAll()
        {
            return _entityRepository;
        }

        public IEnumerable<Entity> Find(Expression<Func<Entity, bool>> predicate)
        {
            return _entityRepository.AsQueryable().Where(predicate).OrderBy(o => o.Id);
        }
    }
    #endregion

    public class SingletonPatternSample : SampleExecute
    {
        public override void Execute()
        {
            var repository = new EntityRepository();
            repository.Add(new Entity() { Id = 1 });
            var result = repository.Find(o => o.Id > 10);
        }
    }
}
