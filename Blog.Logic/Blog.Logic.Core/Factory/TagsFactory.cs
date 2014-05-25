﻿using Blog.DataAccess.Database.Repository;
using Blog.DataAccess.Database.Repository.Interfaces;
using Db = Blog.DataAccess.Database.Entities.Objects;

namespace Blog.Logic.Core.Factory
{
    public class TagsFactory
    {
        private TagsFactory()
        {
        }

        private static TagsFactory _instance;

        public static TagsFactory GetInstance()
        {
            if (_instance == null)
            {
                _instance = new TagsFactory();
                return _instance;
            }
            return _instance;
        }

        public TagsLogic CreateTags()
        {
            ITagRepository tagRepository = new TagRepository();
            return new TagsLogic(tagRepository);
        }
    }
}
