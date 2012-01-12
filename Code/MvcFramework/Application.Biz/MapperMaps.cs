using System.Linq;
using AutoMapper;

namespace Application.Biz
{
    public class MapperMaps
    {
        private static readonly object SyncRoot = new object();
        private static volatile bool _initialized;

        /// <summary>
        ///   Call this to Unit test that all the destination mappings are fully mapped to source properties.
        /// </summary>
        public static void AssertConfigurationIsValid()
        {
            Mapper.AssertConfigurationIsValid();
        }

        public static void Initialize()
        {
            if (_initialized == false)
            {
                lock (SyncRoot)
                {
                    createMappings();
                    _initialized = true;
                }
            }
        }

        /// <summary>
        ///   Create all mappings here
        /// </summary>
        private static void createMappings()
        {
            //Mapper.CreateMap<CallLog, CallViewModel>()
            //    .ForMember(dest => dest.CallParticipants, opt => opt.MapFrom(src => src.Participants))
            //    .ForMember(dest => dest.EditCallParticipant, opt => opt.Ignore())
            //    ;
        }
    }
}