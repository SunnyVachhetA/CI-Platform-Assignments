﻿using CIPlatform.Entities.DataModels;

namespace CIPlatform.DataAccessLayer.Repository.IRepository;
public interface IThemeRepository: IRepository<MissionTheme>
{
    int DeleteTheme(short themeId);
    int DeActivateTheme(short themeId);
}
