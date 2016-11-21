using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using MVCLearn.ModelDbContext;

namespace MVCLearn.Config
{
    public class EntityFramewokConfig
    {
        /// <summary>
        /// EntityFramewok热加载
        /// </summary>
        public static void HeatLoad()
        {
#if !DEBUG
            Database.SetInitializer<LearnDbContext>(null);
#endif
            using (var dbContext = new LearnDbContext())
            {
                var objectContext = ((IObjectContextAdapter) dbContext).ObjectContext;
                var mappingCollection =
                    (StorageMappingItemCollection) objectContext.MetadataWorkspace.GetItemCollection(DataSpace.CSSpace);
                mappingCollection.GenerateViews(new List<EdmSchemaError>());
            }
        }
    }
}