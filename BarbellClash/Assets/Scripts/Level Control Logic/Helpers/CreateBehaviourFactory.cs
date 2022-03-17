namespace ATG.LevelControl
{
    public static class CreateBehaviourFactory
    {
        public static ICreateLevelBehaviour Execute
            (LevelType type,EnvironmentBlock.Factory factory)
        {
            ICreateLevelBehaviour levelBehaviour = null;
            switch (type)
            {
                case LevelType.Line : 
                    levelBehaviour = new LineLevelBehaviour();
                    break;
                case LevelType.Matrix :
                    levelBehaviour = new MatrixLevelBehaviour();
                    break;
                case LevelType.Static :
                    levelBehaviour = new StaticLevelBehaviour();
                    break;
                
                case LevelType.ZenjectLine :
                    levelBehaviour = new ZenjectLineLevelBehaviour<EnvironmentBlock>(factory);
                    break;
                case LevelType.ZenjectMatrix :
                    levelBehaviour = new ZenjectMatrixLevelBehaviour<EnvironmentBlock>(factory);
                    break;
                case LevelType.ZenjectStatic :
                    levelBehaviour = new ZenjectStaticLevelBehaviour<EnvironmentBlock>(factory);
                    break;
            }

            return levelBehaviour;
        }
    }
}