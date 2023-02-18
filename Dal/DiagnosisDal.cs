using Dal.DbModels;
using Entities;
using Entities.SearchParams;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    public class DiagnosisDal:BaseDal<DefaultDbContext, Diagnosis, DiagnosisEntity, int, DiagnosisSearchParams, object>
    {
        protected override bool RequiresUpdatesAfterObjectSaving => false;

    public DiagnosisDal()
    {
    }

    protected internal DiagnosisDal(DefaultDbContext context) : base(context)
    {
    }

    protected override Task UpdateBeforeSavingAsync(DefaultDbContext context, DiagnosisEntity entity, Diagnosis dbObject, bool exists)
    {
            dbObject.AnimalId = dbObject.AnimalId;
            dbObject.Date = dbObject.Date;
            dbObject.Name = dbObject.Name;
            dbObject.VetId = dbObject.VetId;


        return Task.CompletedTask;
    }

    protected override Task<IQueryable<Diagnosis>> BuildDbQueryAsync(DefaultDbContext context, IQueryable<Diagnosis> dbObjects, DiagnosisSearchParams searchParams)
    {


        return Task.FromResult(dbObjects);
    }

    protected override async Task<IList<DiagnosisEntity>> BuildEntitiesListAsync(DefaultDbContext context, IQueryable<Diagnosis> dbObjects, object convertParams, bool isFull)
    {
        return (await dbObjects.ToListAsync()).Select(ConvertDbObjectToEntity).ToList();
    }

    protected override Expression<Func<Diagnosis, int>> GetIdByDbObjectExpression()
    {
        return item => item.Id;
    }

    protected override Expression<Func<DiagnosisEntity, int>> GetIdByEntityExpression()
    {
        return item => item.Id;
    }

    internal static DiagnosisEntity ConvertDbObjectToEntity(Diagnosis dbObject)
    {
        return dbObject == null ? null : new DiagnosisEntity
        {
            Name = dbObject.Name,
            Id = dbObject.Id,
            Date = dbObject.Date,
            AnimalId = dbObject.AnimalId,
            VetId = dbObject.VetId,


        };
    }
}
}
