using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Class_6_Wpf
{
    class WeatherControl : DependencyObject
    {
        public static readonly DependencyProperty TemperatureProperty; // св-во зависимости , название обычного св-ва  + суфикс Property
        private string direction_wind;  // направление ветра
        private int speed_wind;  // скорость ветра
        private Precipitation precipitation; // наличие осадков 
        public int Temperature
        {
            get => (int)GetValue(TemperatureProperty); // в качестве аргумента передаем соот-щее св-во зависимости Temperature + суфикс Property , сокращенная запись в виде лямбда выражения
            set => SetValue(TemperatureProperty, value);
        }

        public string DirectionWind
        {
            get => direction_wind; 
            set => direction_wind = value;
        }
        public int SpeedWind
        {
            get => speed_wind;
            set => speed_wind = value;
        }
        public enum Precipitation // создаем перечисление enum - наличие осадков
        {
            солнечно = 0,
            облачно = 1,
            дождь = 2,
            снег = 3
        }
        public WeatherControl(int temperature, string direction_wind, int speed_wind, Precipitation precipitation)
        {
            this.Temperature = temperature;
            this.DirectionWind = direction_wind;
            this.SpeedWind = speed_wind;
            this.precipitation = precipitation;
        }
        static WeatherControl() // делаем статический конструктор , вызовется только 1 раз, нужен что бы проинициализировать TemperatureProperty через метод Register
        {
            TemperatureProperty = DependencyProperty.Register(
                nameof(Temperature),    // 1й аргумент - название св-ва
                typeof(int),            // 2й аргумент - тип поля
                typeof(WeatherControl),          // 3й аргумент - тип владельца класс Person
                new FrameworkPropertyMetadata(   // 4й аргумент - методада 
                    0,                           // - значение по умолчанию 
                    FrameworkPropertyMetadataOptions.AffectsParentMeasure |             // перечисление флагов для этого берем AffectsMeasure (размер эл-тов при компановке) 
                    FrameworkPropertyMetadataOptions.AffectsRender,                     // еще один флаг AffectsRender 
                    null,                                                               // метод кт будет срабатывать при изменении св-ва (null - действия при изменении нет)
                    new CoerceValueCallback(CoerceTemperature)),                        // коррекция введенного значения
                    new ValidateValueCallback(ValidateTemperature));                    // 5й не обязательный аргумент , метод для валидации 
        }
        private static bool ValidateTemperature(object value)  // метод отвечает за проверку введеных значений, выдает вердикт верно или не верно 
        {
            int t = (int)value;
            if (t >= -50 && t <= 50)
                return true;
            else
                return false;
        }
        private static object CoerceTemperature(DependencyObject d, object baseValue) // метод отвечает за проверку введеных значений, позволяет корректировать значения 
        {
            int t = (int)baseValue;
            if (t >= -50 && t <= 50)
                return true;
            else
                return false;
        }
        
        public string Print()
        {
            return $" {Temperature} {DirectionWind} {SpeedWind} Precipitation";
        }
    }
}
