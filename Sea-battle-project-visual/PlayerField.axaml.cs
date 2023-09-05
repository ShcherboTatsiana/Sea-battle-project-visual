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
                    // "1" - клетка является частью корабля
                    case "1" when playerShipsCounter is 10:
                    {
                        throw new Exception("There is more ships than there must be.");
                    }
                    case "1":
                    {
                        // Находим корабль и получаем его длинну
                        int currShipLength = FindShip(vertIndex, horizIndex);
                        // Увеличиваем счётчик кораблей полученной длинны на 1
                        numberOfShips[currShipLength]++;
                        // Увеличиваем счётчик числа найденных кораблей на 1
                        playerShipsCounter++;
                        // Название корабля = его длина + его порядковый номер (каким по счёту он был найден)
                        string shipName = currShipLength + numberOfShips[currShipLength].ToString();
                        // Заполняем клетки найденного корабля его названием (чтобы в будущем отличать корабли)
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
            
            // Добавляем координаты корабля в массив, в котором
            // они хранятся для использования в будущем
            currShipCoordinates[0, currShipLength] = vertIndex;
            currShipCoordinates[1, currShipLength] = horizIndex;
            
            if (isHorizontal)
                horizIndex++;   // перемещаемся вправо
            else
                vertIndex++;    // перемещаемся вниз
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
            // Сохраняем координаты корабля в специальном массиве
            int shipNum = GetShipNum(shipName);
            playerShipsCoordinates[shipNum, 0, cellNum] = currShipCoordinates[0, cellNum];
            playerShipsCoordinates[shipNum, 1, cellNum] = currShipCoordinates[1, cellNum];
            // Присваиваем клеткам с частью корабля идентификационный номер корабля
            int vertIndex = currShipCoordinates[0, cellNum];
            int horizIndex = currShipCoordinates[1, cellNum];
            playerField[vertIndex, horizIndex] = shipName;
            cellNum++;
        }
    }
    
    // Получаем порядковый номер (из всех кораблей) найденного корабля
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
                if (playerField[vertIndex, horizIndex] is not ("0" or null) 
                    && playerField[vertIndex, horizIndex] != shipName)
                {
                    throw new ArgumentException("There is shouldn't be a ship side to side with another ship");
                }
                
                horizIndex++;
            }

            vertIndex++;
        }
    }
    
    /* Определяет наибольшую возможную длину корабля на момент вызова этой функции.
     * Вычисляется на основании уже найденных кораблей.
     * 
     * Пример:
     * если найден 4, то макс. возможная длина = 3;
     * если найдены все 3 и все 1, то макс. возможная длина = 4 */
    private static int GetMaxPossibleShipLength()
    {
        int shipNum = 4;
        while (numberOfShips[shipNum] == 4 - shipNum + 1)
        {
            shipNum--;
        }

        return shipNum;
    }

    public static string[,] playerField =
    {
        { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" },
        { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" },
        { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" },
        { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" },
        { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" },
        { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" },
        { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" },
        { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" },
        { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" },
        { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" },
        { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" },
        { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" },
    };
        
    public static int playerShipsCounter = 0;
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
