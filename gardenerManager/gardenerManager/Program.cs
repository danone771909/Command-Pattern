using System;
using System.Collections.Generic;

namespace gardenerManager
{
    public interface Task
    {
        void execute();
        void unExecute();
    }

    class PlantTree: Task
    {
        private Gardener gardener;

        public PlantTree(Gardener gardener)
        {
            this.gardener = gardener;
        }

        public void execute()
        {
            gardener.plantTree();
        }

        public void unExecute()
        {
            gardener.cutTree();
        }
    }

    class PlantFlower : Task
    {
        private Gardener gardener;

        public PlantFlower(Gardener gardener)
        {
            this.gardener = gardener;
        }

        public void execute()
        {
            gardener.plantFlower();
        }

        public void unExecute()
        {
            gardener.cutFlower();
        }
    }

    class ChiefGardener
    {
        private Task task;
        private List <Task> _tasks = new List<Task>();
        private int current = 0;

        public ChiefGardener(Task task)
        {
            this.task = task;
        }

        public void addTask()
        {
            task.execute();
            _tasks.Add(task);
            current++;
        }

        public void undo()
        {
            if(current > 0)
            {
                task = _tasks[--current];
                task.unExecute();
            }
        }

        public void redo()
        {
            if (current < _tasks.Count)
            {
                task = _tasks[current++];
                task.execute();
            }
        }

        public void setTask(Task task)
        {
            this.task = task;
        }
    }

    class Gardener
    {
        private String name;

        public Gardener(String name)
        {
            this.name = name;
        }

        public void plantTree()
        {
            Console.WriteLine(name + ": plant Tree");
        }

        public void cutTree()
        {
            Console.WriteLine(name + ": cut Tree");
        }

        public void plantFlower()
        {
            Console.WriteLine(name + ": plant Flower");
        }

        public void cutFlower()
        {
            Console.WriteLine(name + ": cut Flower");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Gardener gardener1 = new Gardener("David");
            Gardener gardener2 = new Gardener("Angelika");

            Task plantTree = new PlantTree(gardener1);
            Task plantFlower = new PlantFlower(gardener2);

            ChiefGardener chiefGardener = new ChiefGardener(plantTree);
            chiefGardener.addTask();

            chiefGardener.setTask(plantFlower);
            chiefGardener.addTask();

            chiefGardener.undo();
            chiefGardener.undo();
            chiefGardener.redo();
            chiefGardener.undo();

            Console.ReadLine();
        }
    }
}
