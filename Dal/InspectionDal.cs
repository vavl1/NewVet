using Dal.DbModels;
using Entities.SearchParams;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Dal
{
    public class InspectionDal : BaseDal<DefaultDbContext, Inspection, InspectionEntity, int, InspectionSearchParams, object>
    {
        protected override bool RequiresUpdatesAfterObjectSaving => false;

        public InspectionDal()
        {
        }

        protected internal InspectionDal(DefaultDbContext context) : base(context)
        {
        }

        protected override Task UpdateBeforeSavingAsync(DefaultDbContext context, InspectionEntity entity, Inspection dbObject, bool exists)
        {
            dbObject.AnimalId = dbObject.AnimalId;
            dbObject.TreatmentId = dbObject.TreatmentId;
            dbObject.VetId = dbObject.VetId;
            dbObject.Description = dbObject.Description;

            return Task.CompletedTask;
        }

        protected override Task<IQueryable<Inspection>> BuildDbQueryAsync(DefaultDbContext context, IQueryable<Inspection> dbObjects, InspectionSearchParams searchParams)
        {


            return Task.FromResult(dbObjects);
        }

        protected override async Task<IList<InspectionEntity>> BuildEntitiesListAsync(DefaultDbContext context, IQueryable<Inspection> dbObjects, object convertParams, bool isFull)
        {
            return (await dbObjects.ToListAsync()).Select(ConvertDbObjectToEntity).ToList();
        }

        protected override Expression<Func<Inspection, int>> GetIdByDbObjectExpression()
        {
            return item => item.Id;
        }

        protected override Expression<Func<InspectionEntity, int>> GetIdByEntityExpression()
        {
            return item => item.Id;
        }

        internal static InspectionEntity ConvertDbObjectToEntity(Inspection dbObject)
        {
            return dbObject == null ? null : new InspectionEntity
            {
                
                Id = dbObject.Id,
             TreatmentId = dbObject.TreatmentId,
             Description = dbObject.Description,
                AnimalId = dbObject.AnimalId,
                VetId = dbObject.VetId,


            };
        }
    }
}
