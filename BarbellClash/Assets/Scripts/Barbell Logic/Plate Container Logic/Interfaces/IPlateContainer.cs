﻿namespace Barbell
{
    public interface IPlateContainer
    {
        ISizeable PlateWithMaxRadius { get; }

        void InitDefaultPlate();
        void AddPlate(PlateLogic plate);

        void StartRotatePlates();
        void StopRotatePlates();
    }
}