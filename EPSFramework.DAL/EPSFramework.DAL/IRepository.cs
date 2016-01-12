//-----------------------------------------------------------------------
// <copyright file="IRepository{Entity}.cs" company="EPapersoft">
//     Copyright ® EPapersoft
// </copyright>
// <Historial>
//      <Entry Date="06/07/2015" Login="JLAlamo">Created</Entry>
// </Historial>
//-----------------------------------------------------------------------
namespace EPSFramework.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// Defines the ability to manage typed models as a domain model service.
    /// </summary>
    /// <typeparam name="Entity">The model to managed</typeparam>
    public interface IRepository<Entity> where Entity : class
    {
        IList<Entity> GetAll();

        Entity Get(dynamic id);

        Entity Get(Expression<Func<Entity, bool>> expr);        

        dynamic Insert(Entity model);

        dynamic Update(Entity model);

        dynamic Delete(Entity model);

        IList<Entity> GetAllBy(params object[] keyValuePairObjects);

        /// <summary>
        /// Gets the value of one column in database. It uses ExecuteScalar, so it's recommended for perfomanceReasons instead of Get.
        /// </summary>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="keyValuePairObjects">The key value pair objects for filtering the query.</param>
        /// <returns></returns>
        dynamic GetValue(string fieldName, params object[] keyValuePairObjects);

        /// <summary>
        /// Maximums the specified field name.
        /// </summary>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="keyValuePairObjects">The key value pair objects for filtering the query.</param>
        /// <returns>Max Aggregate function's value</returns>
        dynamic Max(string fieldName, params object[] keyValuePairObjects);
    }
}