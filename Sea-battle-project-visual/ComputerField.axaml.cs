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

            string shipName = shipType.ToString() + numberOfShips[shipType].ToString();

            CreateShip(shipType);
            // Сортируем по возрастанию массив с координатами текущего корабля
            SelectionSortShipCoord(shipType);
            // Присваиваем клеткам название текущего корабля
            MarkShipCells(shipType, shipName);
            // Помечаем клетки вокруг корабля Х-ми
            // (= нельзя туда ставить часть другого корабля)
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
        /* Выбираем первую клетку корабля (её координаты) */

        // Рандомно выбираем индекс клетки из списка свободных клеток
        int index = randomizer.Next(0, freeCellsCounter);

        // Получаем координаты
        string[] cellCoord = freeCells[index].Split(',');
        int vertIndex = Convert.ToInt32(cellCoord[0]);
        int horizIndex = Convert.ToInt32(cellCoord[1]);

        // Если клетка попала в список неподходящих клеток, начинаем сначала
        if (unfitCells.Contains(vertIndex.ToString() + horizIndex.ToString()))
        {
            CreateShip(shipLength);
            return;
        }

        // Помещаем координаты выбранной клетки во временный массив,
        // который содержит кординаты клеток полученного корабля
        currShipCoordinates[0, 1] = vertIndex;
        currShipCoordinates[1, 1] = horizIndex;

        // Алгоритм ниже рассчитан на корабли длинной > 1
        if (shipLength == 1)
        {
            return;
        }

        int cellNum = 2;               // Порядковый номер текущей подбираемой клетки
        bool isHorizontal = Convert.ToBoolean(randomizer.Next(0, 2));
        bool isToLeftOrTop = Convert.ToBoolean(randomizer.Next(0, 2));
        bool isChangeOfSideCanHelp = true;
        bool isChangeOfDirectionCanHelp = true;
        int newShipSideSign = 1;       // указывает сторону, в которой выбирается клетка:
                                       // 1 = лево (верх); -1 = право (низ)

        // Если было рандомно выбрано движение влево (вверх)
        if (isToLeftOrTop)
        {
            newShipSideSign *= -1;
        }

        // Пока количество выбранных клеток != длине создаваемого корабля
        while (cellNum <= shipLength)
        {
            switch (isHorizontal)
            {
                // Клетка по горизонтали пуста
                case true when compField[vertIndex, horizIndex + newShipSideSign] == "0":
                    {
                        horizIndex += newShipSideSign;
                        currShipCoordinates[0, cellNum] = vertIndex;
                        currShipCoordinates[1, cellNum] = horizIndex;
                        break;
                    }
                // Достигли границы поля или неподходящей клетки
                // Пытаемся выбрать клетку с другой стороны, если этого ещё не делали
                case true when isChangeOfSideCanHelp:
                    {
                        horizIndex = currShipCoordinates[1, 1];
                        newShipSideSign *= -1;
                        isChangeOfSideCanHelp = false;
                        continue;
                    }
                // Клетка по вертикали пуста
                case false when compField[vertIndex + newShipSideSign, horizIndex] == "0":
                    {
                        vertIndex += newShipSideSign;
                        currShipCoordinates[0, cellNum] = vertIndex;
                        currShipCoordinates[1, cellNum] = horizIndex;
                        break;
                    }
                // Достигли границы поля или неподходящей клетки
                // Пытаемся выбрать клетку с другой стороны, если этого ещё не делали 
                case false when isChangeOfSideCanHelp:
                    {
                        vertIndex = currShipCoordinates[0, 1];
                        newShipSideSign *= -1;
                        isChangeOfSideCanHelp = false;
                        continue;
                    }
                // Пытаемся расположить корабль в другом положении,
                // если этого ещё не делали
                case true or false when isChangeOfDirectionCanHelp:
                    {
                        vertIndex = currShipCoordinates[0, 1];
                        horizIndex = currShipCoordinates[1, 1];
                        newShipSideSign *= -1;
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
                        unfitCells.Add(currShipCoordinates[0, 1].ToString() + currShipCoordinates[1, 1].ToString());
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
            // Присваиваем клеткам с частью корабля идентификационный номер корабля
            int vertIndex = currShipCoordinates[0, cellNum];
            int horizIndex = currShipCoordinates[1, cellNum];
            compField[vertIndex, horizIndex] = shipName.ToString();

            // Сохраняем координаты корабля в постоянном массиве,
            // хранящем координаты клеток всех кораблей
            compShipsCoordinates[compShipsCounter, 0, cellNum] = currShipCoordinates[0, cellNum];
            compShipsCoordinates[compShipsCounter, 1, cellNum] = currShipCoordinates[1, cellNum];

            // Убираем эту клетку из списка клеток,
            // в которых можно расположить часть корабля / корабль
            freeCells.Remove(vertIndex.ToString() + "," + horizIndex.ToString());
            freeCellsCounter--;

            cellNum++;
        }
    }

    /* Заполняет клетки вокруг созданного корабля "Х"-ми
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
                if (compField[vertIndex, horizIndex] == "0")
                {
                    compField[vertIndex, horizIndex] = "Х";

                    freeCells.Remove(vertIndex.ToString() + "," + horizIndex.ToString());
                    freeCellsCounter--;
                }

                horizIndex++;
            }

            vertIndex++;
        }
    }

    public static Random randomizer = new Random();
    public static string[,] compField =
    {
        { "X", "X", "X", "X", "X", "X", "X", "X", "X", "X", "X", "X" },
        { "X", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "X" },
        { "X", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "X" },
        { "X", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "X" },
        { "X", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "X" },
        { "X", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "X" },
        { "X", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "X" },
        { "X", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "X" },
        { "X", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "X" },
        { "X", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "X" },
        { "X", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "X" },
        { "X", "X", "X", "X", "X", "X", "X", "X", "X", "X", "X", "X" },
    };
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

    // Список незанятых клеток на поле
    private static List<string> freeCells = new List<string>();
    private static int freeCellsCounter = 100;

    public static void FillTheFreeCellsList()
    {
        freeCells.AddRange(new[]
        {
            "1,1", "1,2", "1,3", "1,4", "1,5", "1,6", "1,7", "1,8", "1,9", "1,10",
            "2,1", "2,2", "2,3", "2,4", "2,5", "2,6", "2,7", "2,8", "2,9", "2,10",
            "3,1", "3,2", "3,3", "3,4", "3,5", "3,6", "3,7", "3,8", "3,9", "3,10",
            "4,1", "4,2", "4,3", "4,4", "4,5", "4,6", "4,7", "4,8", "4,9", "4,10",
            "5,1", "5,2", "5,3", "5,4", "5,5", "5,6", "5,7", "5,8", "5,9", "5,10",
            "6,1", "6,2", "6,3", "6,4", "6,5", "6,6", "6,7", "6,8", "6,9", "6,10",
            "7,1", "7,2", "7,3", "7,4", "7,5", "7,6", "7,7", "7,8", "7,9", "7,10",
            "8,1", "8,2", "8,3", "8,4", "8,5", "8,6", "8,7", "8,8", "8,9", "8,10",
            "9,1", "9,2", "9,3", "9,4", "9,5", "9,6", "9,7", "9,8", "9,9", "9,10",
            "10,1", "10,2", "10,3", "10,4", "10,5", "10,6", "10,7", "10,8", "10,9", "10,10",
        });
    }

    // Список неподходящих на момент выполнения функции CreateShip() клеток
    private static List<string> unfitCells = new List<string>();
}