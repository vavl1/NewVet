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
    public class TreatmetsDal : BaseDal<DefaultDbContext, Treatment, TreatmentEntity, int, TreatmentSearchParams, object>
    {
        protected override bool RequiresUpdatesAfterObjectSaving => true;

        public TreatmetsDal()
        {
        }

        protected internal TreatmetsDal(DefaultDbContext context) : base(context)
        {
        }

        protected override Task UpdateBeforeSavingAsync(DefaultDbContext context, TreatmentEntity entity, Treatment dbObject, bool exists)
        {
            
            dbObject.IsDischarged = entity.IsDischarged;
            dbObject.Description = entity.Description;
            dbObject.InspectionId = entity.InspectionId;
            dbObject.DiagnosId = entity.DiagnosId;
          


            return Task.CompletedTask;
        }

        protected override Task<IQueryable<Treatment>> BuildDbQueryAsync(DefaultDbContext context, IQueryable<Treatment> dbObjects, TreatmentSearchParams searchParams)
        {
           
            if (searchParams.IsDischarged != null)
            {
                dbObjects = dbObjects.Where(i => i.IsDischarged != searchParams.IsDischarged);
            }

            return Task.FromResult(dbObjects);
        }

        protected override async Task<IList<TreatmentEntity>> BuildEntitiesListAsync(DefaultDbContext context, IQueryable<Treatment> dbObjects, object convertParams, bool isFull)
        {
            dbObjects = dbObjects.Include(i => i.Diagnos);
            dbObjects = dbObjects.Include(i => i.Inspection);
            
            return (await dbObjects.ToListAsync()).Select(ConvertDbObjectToEntity).ToList();
        }

        protected override Expression<Func<Treatment, int>> GetIdByDbObjectExpression()
        {
            return item => item.Id;
        }

        protected override Expression<Func<TreatmentEntity, int>> GetIdByEntityExpression()
        {
            return item => item.Id;
        }

        internal static TreatmentEntity ConvertDbObjectToEntity(Treatment dbObject)
        {
            return dbObject == null ? null : new TreatmentEntity
            {
                Id = dbObject.Id,
                IsDischarged = dbObject.IsDischarged,
                InspectionId = dbObject.InspectionId,
                DiagnosId = dbObject.DiagnosId,
                Description = dbObject.Description,
               Inspection = InspectionDal.ConvertDbObjectToEntity(dbObject.Inspection),
                Diagnos = DiagnosisDal.ConvertDbObjectToEntity(dbObject.Diagnos),


            };
        }
    }
}
