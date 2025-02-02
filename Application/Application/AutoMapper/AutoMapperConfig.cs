﻿using Application.AutoMapper.MapProfiles;
using AutoMapper;

namespace Application.AutoMapper;

public class AutoMapperConfig
{
    public static MapperConfiguration RegisterMaps()
    {
        return new MapperConfiguration(config =>
        {
            config.AddProfile<AuthenticationMapProfile>();
        });
    }
}