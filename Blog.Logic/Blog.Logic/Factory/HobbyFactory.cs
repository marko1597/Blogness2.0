﻿using Blog.DataAccess.Database.Repository;

namespace Blog.Logic.Core.Factory
{
    public class HobbyFactory
    {
        private HobbyFactory()
        {
        }

        private static HobbyFactory _instance;

        public static HobbyFactory GetInstance()
        {
            if (_instance == null)
            {
                _instance = new HobbyFactory();
                return _instance;
            }
            return _instance;
        }

        public HobbyLogic CreateHobby()
        {
            IHobbyRepository hobbyRepository = new HobbyRepository();
            return new HobbyLogic(hobbyRepository);
        }
    }
}
