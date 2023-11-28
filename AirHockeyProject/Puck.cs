﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace AirHockeyProject
{

    public class Puck
    {
        public ObjPosition ObjPosition { get; set; }

        private readonly DispatcherTimer _collisionTimer;
        private readonly FieldObject[] _objectsOnField;
        private MovingLine _movingline = null;

        private const int MaxBorder = 500;

        public Puck(UIElement movingObject, Action<UIElement, double> setLeft, Action<UIElement, double> setTop, FieldObject[] objects)
        {
            ObjPosition = new ObjPosition(movingObject, setLeft, setTop);
            _collisionTimer = new DispatcherTimer();
            _objectsOnField = objects;

            DispatcherTimerInit();
        }

        private void DispatcherTimerInit()
        {
            _collisionTimer.Interval = TimeSpan.FromMilliseconds(10.0);
            _collisionTimer.Tick += CheckCollision;
            _collisionTimer.Start();
        }

        private void CheckCollision(object sender, EventArgs e)
        {
            CheckCollisionObjects();
            CheckCollisionBorders();
        }
        private void CheckCollisionObjects()
        {
            foreach (FieldObject obj in _objectsOnField)
            {
                if (IsCollision(obj))
                {
                    _collisionTimer.Stop();
                    CreateLineMoving(obj.ObjPosition.Pose, this.ObjPosition.Pose);
                    _collisionTimer.Start();
                    break;
                }
            }
        }
        private void CheckCollisionBorders()
        {

            if (ObjPosition.Pose.X > MaxBorder)
                CalculatePoint(ObjPosition.Pose, "Up_X");
            else if (ObjPosition.Pose.X < 0)
                CalculatePoint(ObjPosition.Pose, "Down_X");

            if (ObjPosition.Pose.Y > MaxBorder)
                CalculatePoint(ObjPosition.Pose, "Up_Y");
            else if (ObjPosition.Pose.Y < 0)
                CalculatePoint(ObjPosition.Pose, "Down_Y");
        }
        private bool IsCollision(FieldObject objCollision)
        {
            double lenBetweenCenters = (objCollision.ObjPosition.Pose - this.ObjPosition.Pose).Length;
            double radiusObj = (objCollision.ObjPosition.MovingObject as FrameworkElement).ActualWidth / 2;
            double radiusPuck = (this.ObjPosition.MovingObject as FrameworkElement).ActualWidth / 2;

            if ((int)lenBetweenCenters == radiusPuck + radiusObj)
                return true;

            return false;
        }

        private void CalculatePoint(Point pointOne, string border)
        {
            Point borderPoint = new Point();
            double offset = (ObjPosition.MovingObject as FrameworkElement).ActualWidth;
            switch (border)
            {
                case "Down_Y":
                    borderPoint = new Point(0, pointOne.Y - offset);
                    break;
                case "Down_X":
                    borderPoint = new Point(pointOne.X - offset, 0);
                    break;
                case "Up_Y":
                    borderPoint = new Point(0, pointOne.Y + offset);
                    break;
                case "Up_X":
                    borderPoint = new Point(pointOne.X + offset, 0);
                    break;

            }
            CreateLineMoving(borderPoint, pointOne);
        }
        public void CreateLineMoving(Point pointOne, Point pointTwo)
        {
            double tangentInclination = (pointOne.Y - pointTwo.Y) / (pointOne.X - pointTwo.X);
            double YaxisOffset = pointOne.Y - tangentInclination * pointOne.X;
            string mode;

            if (pointOne.X < pointTwo.X)
                mode = "Forward";
            else
                mode = "Back";

            _movingline?.MovingTimer.Stop();
            _movingline = new MovingLine(this, tangentInclination, YaxisOffset, mode);
            _movingline.MovingTimer.Start();
        }

    }

}


