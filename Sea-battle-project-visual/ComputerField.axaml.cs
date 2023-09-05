using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using static sea_battle_project.PlayerField;

namespace sea_battle_project;

[SuppressMessage("ReSharper", "CommentTypo")]
public static class ComputerField
{ 
    // Заполняет поле кораблями
    public static void FillTheCompFieldWithShips()
    {
        int shipType = 4;
        while (shipType > 0)
        {
            numberOfShips[shipType]++;
            compShipsCounter++;
            string shipName = shipType + numberOfShips[shipType].ToString();
            CreateShip(shipType);
            MarkShipCells(shipType, shipName);
            SelectionSortShipCoord(shipType);
            MarkCellsAroundTheShip(shipType);

            // Проверяем, было ли расставлено на поле необходимое кол-во кораблей текущего типа
            if (numberOfShips[shipType] == 4 - shipType + 1)
            {
                shipType--;
            }
        }
    }

    // Расставляет на поле корабль указанной длины
    private static void CreateShip(int shipLength)
    {
        // Выбираем рандомно первую клетку
        int vertIndex = randomizer.Next(1, 11);
        int horizIndex = randomizer.Next(1, 11);
        
        // Если клетка попала в список неподходящих клеток, начинаем сначала
        if (unfitCells.Contains(vertIndex + horizIndex.ToString()))
        {
            CreateShip(shipLength);
            return;
        }
        
        // Если клетка уже занята (там стоит корабль или же эта клетка находится возле корабля),
        // начинаем сначала
        if (compField[vertIndex, horizIndex] != "0")
        {
            unfitCells.Add(vertIndex + horizIndex.ToString());
            CreateShip(shipLength);
            return;
        }

        // Помещаем координаты выбранной клетки в массив,
        // который содержит кординаты клеток полученного корабля
        currShipCoordinates[0, 1] = vertIndex;
        currShipCoordinates[1, 1] = horizIndex;

        // Алгоритм ниже рассчитан на корабли > 1
        if (shipLength == 1)
        {
            return;
        }

        int cellNum = 2;        // Порядковый номер текущей подбираемой клетки
        bool isHorizontal = Convert.ToBoolean(randomizer.Next(0, 2));
        bool isToLeftOrTop = Convert.ToBoolean(randomizer.Next(0, 2));
        bool isChangeOfSideCanHelp = true;
        bool isChangeOfDirectionCanHelp = true;
        int sideSign = 1;       // указывает сторону, в которой выбирается клетка: лево (верх) или право (низ)

        // Если было рандомно выбрано движение влево (вверх)
        if (isToLeftOrTop)
        {
            sideSign *= -1;
        }
        
        // Пока количество выбранных клеток != длинне создаваемого корабля
        while (cellNum <= shipLength)
        {
            switch (isHorizontal)
            {
                // Клетка по горизонтали пуста
                case true when compField[vertIndex, horizIndex + sideSign] == "0":
                {
                    horizIndex += sideSign;
                    currShipCoordinates[0, cellNum] = vertIndex;
                    currShipCoordinates[1, cellNum] = horizIndex;
                    break;
                }
                // Достигли границы поля или неподходящей клетки
                // Пытаемся выбрать клетку с другой стороны, если этого ещё не делали
                case true when isChangeOfSideCanHelp:
                {
                    horizIndex = currShipCoordinates[1, 1];
                    sideSign *= -1;
                    isChangeOfSideCanHelp = false;
                    continue;
                }
                // Клетка по вертикали пуста
                case false when compField[vertIndex + sideSign, horizIndex] == "0":
                {
                    vertIndex += sideSign;
                    currShipCoordinates[0, cellNum] = vertIndex;
                    currShipCoordinates[1, cellNum] = horizIndex;
                    break;
                }
                // Достигли границы поля или неподходящей клетки
                // Пытаемся выбрать клетку с другой стороны, если этого ещё не делали 
                case false when isChangeOfSideCanHelp:
                {
                    vertIndex = currShipCoordinates[0, 1];
                    sideSign *= -1;
                    isChangeOfSideCanHelp = false;
                    continue;
                }
                // Пытаемся расположить корабль в другом положении,
                // если этого ещё не делали
                case true or false when isChangeOfDirectionCanHelp:
                {
                    vertIndex = currShipCoordinates[0, 1];
                    horizIndex = currShipCoordinates[1, 1];
                    sideSign *= -1;
                    isHorizontal = !isHorizontal;
                    isChangeOfSideCanHelp = true;
                    isChangeOfDirectionCanHelp = false;
                    cellNum = 2;
                    continue;
                }
                // Если корабль не удалось расположить ни в каком положении (вертикальном
                // или горизонтальном), значит, первая клетка была выбрана неверно.
                // Помещаем координаты этой клетки в список неподходящих клеток и
                // начинаем процесс выбора первой клетки и создания корабля сначала
                default:
                {
                    unfitCells.Add(currShipCoordinates[0, 1] + currShipCoordinates[1, 1].ToString());
                    CreateShip(shipLength);
                    return;
                }
            }
            
            cellNum++;
        }
        
        unfitCells.Clear();
    }

    /* Заполняет клетки корабля, расположенного на поле перед вызовом этой фукнции,
     * "названием корабля + его порядковым номером", используя координаты,
     * добавленные в двухмерный массив currShipCoordinates */
    private static void MarkShipCells(int shipLength, string shipName)
    {
        int cellNum = 1;
        while (cellNum <= shipLength)
        {
            // Сохраняем координаты корабля в специальном массиве
            compShipsCoordinates[compShipsCounter - 1, 0, cellNum] = currShipCoordinates[0, cellNum];
            compShipsCoordinates[compShipsCounter - 1, 1, cellNum] = currShipCoordinates[1, cellNum];
            // Присваиваем клеткам с частью корабля идентификационный номер корабля
            int vertIndex = currShipCoordinates[0, cellNum];
            int horizIndex = currShipCoordinates[1, cellNum];
            compField[vertIndex, horizIndex] = shipName;
            cellNum++;
        }
    }
    
    /* Сортирует по возрастанию массив currShipCoordinates, содержащий координаты
     * клеток созданного перед вызовом этой фукнции корабля */
    private static void SelectionSortShipCoord(int shipLength)
    {
        if (shipLength == 1)
        {
            return;
        }

        // Индекс ряда, который надо сортировать (в котором координаты отличаются)
        int toSortRow = currShipCoordinates[0, 1] == currShipCoordinates[0, 2] ? 1 : 0;
        for (int currIndex = 1; currIndex < shipLength; currIndex++)
        {
            int minElemIndex = currIndex;
            int sepIndex = currIndex;     // Индекс разделителя, слева от которого: отсортированные элементы
            while (sepIndex < shipLength)
            {
                sepIndex++;
                if (currShipCoordinates[toSortRow, sepIndex] < 
                    currShipCoordinates[toSortRow, minElemIndex])
                {
                    minElemIndex = sepIndex;
                }
            }

            (currShipCoordinates[toSortRow, currIndex], currShipCoordinates[toSortRow, minElemIndex]) = 
                (currShipCoordinates[toSortRow, minElemIndex], currShipCoordinates[toSortRow, currIndex]);
        }
    }

    /* Заполняет клетки вокруг созданного корабля "1"-ми
     * (= в этих клетках нельзя располагать части другого корабля) */
    private static void MarkCellsAroundTheShip(int shipLength)
    {
        // Рассматриваем корабль и окружающее его пространство (клетки)
        // как прямоугольник, по которому и проходимся
        int vertIndex = currShipCoordinates[0, 1] - 1;
        while (vertIndex <= currShipCoordinates[0, shipLength] + 1)
        {
            int horizIndex = currShipCoordinates[1, 1] - 1;
            while (horizIndex <= currShipCoordinates[1, shipLength] + 1)
            {
                // Выбранная клетка не является частью корабля (=> принадлежит его окружению)
                if (compField[vertIndex, horizIndex] is "0" or null)
                {
                    compField[vertIndex, horizIndex] = "1";
                }

                horizIndex++;
            }

            vertIndex++;
        }
    }
    
    public static Random randomizer = new Random();
    public static string[,] compField = new string[12, 12];    // Поле компьютера
    private static int[] numberOfShips = { 0, 0, 0, 0, 0 };     // Число кораблей каждого типа 
    public static int compShipsCounter = 0;
    
    /* Число клеток каждого из кораблей
     * Координата по горизонтали - длина корабля
     * Координата по вертикали - порядковый номер */
    public static int[,] compShipsStatus =
    {
        { 0, 0, 0, 0, 0 },
        { 0, 1, 2, 3, 4 },
        { 0, 1, 2, 3, 0 },
        { 0, 1, 2, 0, 0 },
        { 0, 1, 0, 0, 0 }
    };
    
    // Массив для хранения координат каждого из кораблей
    public static int[,,] compShipsCoordinates = new int[11, 2, 5];
    
    // Список неподходящих на момент выполнения функции CreateShip() клеток
    private static List<string> unfitCells = new List<string>();
}