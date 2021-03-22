﻿using System.Collections.Generic;

/*
 * 2.2.1. GAME
 * Создайте иерархию классов и пропишите ключевые методы для компьютерной игры (без реализации функционала). 
 * Суть игры:
 * • Игрок может передвигаться по прямоугольному полю размером Width на Height;
 * • На поле располагаются бонусы (яблоко, вишня и т.д.), которые игрок может подобрать для поднятия каких-либо характеристик;
 * • За игроком охотятся монстры (волки, медведи и т.д.), которые могут передвигаться по карте по какому-либо алгоритму;
 * • На поле располагаются препятствия разных типов (камни, деревья и т.д.), которые игрок и монстры должны обходить.
 * 
 * Цель игры — собрать все бонусы и не быть «съеденным» монстрами.
 * При желании объекты-бонусы могут быть заменены вами на аналогичные (патроны, канистры с бензином, монетки),
 * также, как и враги (роботы, мумии, зомби). Включайте фантазию!
 * 
 * Вариант со * - подумайте над реализацией консольного геймплея вашей игры. 
 * Как он может выглядеть? Каким образом отрисовать поле? Добавьте классы визуализации поля, препятствий, врагов и бонусов на нём.
 * Вариант с ** - попробуйте сделать играбельную версию вашего проекта. 
 * На данном этапе не стоит заморачиваться над балансом, интересностью геймплея или удобством.
 * 
 */
namespace Task_2_2_1
{
    public class GameHandler
    {
        List<AbstractMonster> monsters = new List<AbstractMonster>();
        List<AbstactBonus> bonuses = new List<AbstactBonus>();
        List<AbstractObstacle> obstacles = new List<AbstractObstacle>();

        // В workingSpace мы будем хранить все объекты на сцене. Там же, с помощью функций врагов и игрока 
        // (IMovable) будет происходить перемещение
        ObjectOnScene[,] workingSpace/* = new ObjectOnScene[5, 5]*/;

        // Здесь можно инициализировать объекты, или же передавать сюда уже целую сцену
        public GameHandler()
        {

        }

        // Он же Flip, он же Update
        public void NextFrame()
        {

        }
    }
}