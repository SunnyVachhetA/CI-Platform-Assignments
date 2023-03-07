﻿using CIPlatform.Entities.DataModels;

namespace CIPlatform.DataAccessLayer.Repository.IRepository;
public interface IMissionRepository : IRepository<Mission>
{
    List<Mission> GetAllMissions();
}
