using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq.Expressions;
using System.Security.Cryptography;

namespace sea_battle_project;

[SuppressMessage("ReSharper", "CommentTypo")]
public static class PlayerField
{
    // Проверяет, верно ли расставлены корабли на поле
    public static void PlayerFieldCheck()
    {
        // Проверяем все клетки, начиная c верхнего левого угла [1,1]
        for (int vertIndex = 1; vertIndex <= 10; vertIndex++)
        {
            for (int horizIndex = 1; horizIndex <= 10; horizIndex++)
            {
                // Работаем с содержимым текущей клетки
                switch (playerField[vertIndex, horizIndex])
                {
                    // "1" = клетка является частью корабля
                    case "1" when playerShipsCounter is 10:
                        {
                            throw new Exception("There is more ships than there must be.");
                        }
                    case "1":
                        {
                            // Находим корабль и получаем его длинну
                            int currShipLength = FindShip(vertIndex, horizIndex);

                            // Увеличиваем счётчик кораблей полученной длины на 1
                            numberOfShips[currShipLength]++;

                            // Увеличиваем счётчик числа найденных кораблей на 1
                            playerShipsCounter++;

                            // Название корабля = его длина + его порядковый номер (каким по счёту он был найден)
                            string shipName = currShipLength.ToString() + numberOfShips[currShipLength].ToString();

                            // Сортируем массив с координатами корабля по возрастанию
                            // (необходимо для верной работы нижеследующих 2-х методов)
                            SelectionSortShipCoord(currShipLength);

                            // Заполняем клетки найденного корабля его названием
                            // (чтобы в будущем отличать корабли)
                            MarkShipCells(currShipLength, shipName);

                            // Проверяем, пусты ли клетки вокруг корабля
                            CheckCellsAroundTheShip(currShipLength, shipName);
                            break;
                        }
                }
            }
        }

        // Не все корабли были расставлены на поле
        if (playerShipsCounter < 10)
        {
            throw new Exception("There is less ships than there must be.");
        }
    }

    // Ищет корабль, основываясь на найденной клетке, и возвращает его длинну
    private static int FindShip(int vertIndex, int horizIndex)
    {
        // Проверяем клетку справа. Если там находится часть корабля, то корабль
        // расположен горизонтально. Если нет, то вертикально или перед нами 1
        // (но для неё направление не важно)
        bool isHorizontal = playerField[vertIndex, horizIndex + 1] == "1" ? true : false;
        int currShipLength = 0;
        int maxPossibleShipLength = GetMaxPossibleShipLength();

        // Пока находимые клетки являются частями корабля
        while (playerField[vertIndex, horizIndex] == "1")
        {
            currShipLength++;

            // Длина корабля превышает максимально возможную => ошибка
            if (currShipLength > maxPossibleShipLength)
            {
                throw new ArgumentException("The length (= " + currShipLength +
                                            ") of the ship (cell [" + vertIndex + "," + horizIndex
                                            + "]) is greater than " + maxPossibleShipLength + ".");
            }

            // Добавляем координаты корабля в массив,
            // в котором они хранятся для использования в будущем
            currShipCoordinates[0, currShipLength] = vertIndex;
            currShipCoordinates[1, currShipLength] = horizIndex;

            if (isHorizontal)
            {
                horizIndex++;   // перемещаемся вправо
            }
            else
            {
                vertIndex++;    // перемещаемся вниз
            }
        }

        // Проверяем, не были ли уже найдены все корабли такой же длинны,
        // что и только найденный корабль
        if (numberOfShips[currShipLength] > 4 - currShipLength + 1)
        {
            throw new Exception("There is more ships of length " +
                                currShipLength + " than there must be.");
        }

        return currShipLength;
    }

    /* Заполняет клетки корабля, расположенного на поле перед вызовом этой фукнции,
     * "названием корабля + его порядковым номером", используя координаты его частей,
     * добавленные в двухмерный массив currShipCoordinates */
    private static void MarkShipCells(int shipLength, string shipName)
    {
        int cellNum = 1;
        while (cellNum <= shipLength)
        {
            // Присваиваем клеткам с частью корабля идентификационный номер корабля
            int vertIndex = currShipCoordinates[0, cellNum];
            int horizIndex = currShipCoordinates[1, cellNum];
            playerField[vertIndex, horizIndex] = shipName;

            // Сохраняем координаты корабля в постоянном массиве,
            // хранящем координаты клеток всех кораблей
            int shipNum = GetShipNum(shipName);
            playerShipsCoordinates[shipNum, 0, cellNum] = currShipCoordinates[0, cellNum];
            playerShipsCoordinates[shipNum, 1, cellNum] = currShipCoordinates[1, cellNum];

            cellNum++;
        }
    }

    /* Получаем порядковый номер найденного корабля
    * 
    * shipNumList =
    * { "None", "41", "31", "32", "21", "22", "23", "11", "12", "13", "14" }; */
    public static int GetShipNum(string shipName)
    {
        int index = 0;
        while (index < shipNumList.Length)
        {
            if (shipNumList[index] == shipName)
            {
                break;
            }

            index++;
        }

        return index;
    }

    // Проверяет, пусты ли клетки вокруг найденного корабля
    private static void CheckCellsAroundTheShip(int shipLength, string shipName)
    {
        // Рассматриваем корабль и окружающее его пространство (клетки)
        // как прямоугольник, по которому и проходимся
        int vertIndex = currShipCoordinates[0, 1] - 1;
        while (vertIndex <= currShipCoordinates[0, shipLength] + 1)
        {
            int horizIndex = currShipCoordinates[1, 1] - 1;
            while (horizIndex <= currShipCoordinates[1, shipLength] + 1)
            {
                // Выбранная клетка не является частью проверяемого корабля или пустой клеткой
                if (playerField[vertIndex, horizIndex] is not ("0" or "X")
                    && playerField[vertIndex, horizIndex] != shipName)
                {
                    throw new ArgumentException("There is shouldn't be a ship side to side with another ship");
                }

                horizIndex++;
            }

            vertIndex++;
        }
    }

    // Возвращает наибольшую из всех доступных длин
    // (доступные = не все корабли таких длин были найдены)
    private static int GetMaxPossibleShipLength()
    {
        int shipNum = 4;
        while (numberOfShips[shipNum] == 4 - shipNum + 1)
        {
            shipNum--;
        }

        return shipNum;
    }

    /* Сортирует по возрастанию массив currShipCoordinates, содержащий
     * координаты клеток созданного перед вызовом этой фукнции корабля */
    public static void SelectionSortShipCoord(int shipLength)
    {
        if (shipLength == 1)
        {
            return;
        }

        // Индекс ряда, который надо сортировать (в котором координаты отличаются)
        // 1 - гориз; 0 - верт
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

    public static string[,] playerField =
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

    // Счётчик кораблей игрока
    public static int playerShipsCounter = 0;

    // Число кораблей каждого типа
    private static int[] numberOfShips = { 0, 0, 0, 0, 0 };

    /* Демонстрирует, сколько клеток каждого из кораблей было найдено
     * 
     * Координата по горизонтали - длина корабля
     * Координата по вертикали - порядковый номер */
    public static int[,] playerShipsStatus =
    {
        { 0, 0, 0, 0, 0 },
        { 0, 1, 2, 3, 4 },
        { 0, 1, 2, 3, 0 },
        { 0, 1, 2, 0, 0 },
        { 0, 1, 0, 0, 0 }
    };

    // Массив для хранения координат найденного корабля
    public static int[,] currShipCoordinates = new int[2, 5];

    // Массив для хранения координат каждого из кораблей
    public static int[,,] playerShipsCoordinates = new int[11, 2, 5];

    // Масссив, с помощью которого можно определить порядковый номер найденного корабля,
    // чтобы поместить его координаты в массив playerShipsCoordinates
    private static string[] shipNumList = { "None", "41", "31", "32", "21", "22", "23", "11", "12", "13", "14" };
}
