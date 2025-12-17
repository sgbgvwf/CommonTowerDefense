using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * 命名空间：RPG.Timer
 * 包含与计时器相关的类，提供基于时间（秒）和基于帧的计时功能，
 * 以及用于集中管理这些计时器的管理器类。
 */
namespace Concorde.Timer
{
    /// <summary>
    /// 基于时间（秒）的计时器。
    /// </summary>
    /// <remarks>
    /// 用于跟踪指定持续时间内的时间流逝，可判断是否已完成计时，并支持重启计时器。
    /// </remarks>
    public class Timer
    {
        /// <summary>
        /// 计时器的总持续时间（秒）。
        /// </summary>
        public float Duration { get; private set; }

        /// <summary>
        /// 计时器的开始时间（基于 <see cref="Time.time"/>，即游戏运行的总秒数）。
        /// </summary>
        public float StartTime { get; private set; }

        /// <summary>
        /// 初始化计时器的持续时间，并记录当前开始时间。
        /// </summary>
        /// <param name="duration">计时器的总持续时间（秒）。</param>
        public Timer(float duration)
        {
            Duration = duration;
            StartTime = Time.time;
        }

        /// <summary>
        /// 重启计时器，可选择指定新的持续时间。
        /// </summary>
        /// <param name="newDuration">
        /// 可选参数，新的持续时间（秒）。
        /// 若为 <c>null</c>，则沿用原有持续时间。
        /// </param>
        public void ReStart(float? newDuration = null)
        {
            Duration = newDuration ?? Duration;
            StartTime = Time.time;
        }

        /// <summary>
        /// 已流逝的时间（当前时间 - 开始时间，单位：秒）。
        /// </summary>
        public float Elapsed => Time.time - StartTime;

        /// <summary>
        /// 计时器是否已完成（已流逝时间 >= 总持续时间）。
        /// </summary>
        public bool IsFinished => Elapsed >= Duration;
    }

    /// <summary>
    /// 基于帧的计时器。
    /// </summary>
    /// <remarks>
    /// 用于跟踪指定帧数内的帧流逝，可判断是否已完成计时，支持转换为时间（秒），并支持重启计时器。
    /// </remarks>
    public class FrameTimer
    {
        /// <summary>
        /// 计时器的总帧数。
        /// </summary>
        public int MaxFrames { get; private set; }

        /// <summary>
        /// 计时器的开始帧（基于 <see cref="Time.frameCount"/>，即游戏运行的总帧数）。
        /// </summary>
        public int StartFrame { get; private set; }

        /// <summary>
        /// 已流逝的帧数（当前帧 - 开始帧）。
        /// </summary>
        public int ElapsedFrames => Time.frameCount - StartFrame;

        /// <summary>
        /// 计时器是否已完成（已流逝帧数 >= 总帧数）。
        /// </summary>
        public bool IsFinished => ElapsedFrames >= MaxFrames;

        /// <summary>
        /// 已流逝的时间（已流逝帧数 * 每帧时间 <see cref="Time.deltaTime"/>，单位：秒）。
        /// </summary>
        public float ElapsedTime => ElapsedFrames * Time.deltaTime;

        /// <summary>
        /// 总持续时间（总帧数 * 每帧时间 <see cref="Time.deltaTime"/>，单位：秒）。
        /// </summary>
        public float DurationTime => MaxFrames * Time.deltaTime;

        /// <summary>
        /// 初始化计时器的总帧数，并记录当前开始帧。
        /// </summary>
        /// <param name="maxFrames">计时器的总帧数。</param>
        public FrameTimer(int maxFrames)
        {
            MaxFrames = maxFrames;
            StartFrame = Time.frameCount;
        }

        /// <summary>
        /// 重启计时器，可选择指定新的总帧数。
        /// </summary>
        /// <param name="newMaxFrames">
        /// 可选参数，新的总帧数。
        /// 若为 <c>null</c>，则沿用原有总帧数。
        /// </param>
        public void ReStart(int? newMaxFrames = null)
        {
            MaxFrames = newMaxFrames ?? MaxFrames;
            StartFrame = Time.frameCount;
        }
    }

    /// <summary>
    /// 计时器管理器。
    /// </summary>
    /// <remarks>
    /// 用于集中管理多个基于时间（<see cref="Timer"/>）和基于帧（<see cref="FrameTimer"/>）的计时器，
    /// 提供启动、检查状态、获取信息、重启、移除等统一操作接口。
    /// </remarks>
    public class TimerManager
    {
        // 存储基于时间的计时器字典（键：计时器唯一标识，值：对应的Timer实例）
        private Dictionary<string, Timer> _timers = new();

        // 存储基于帧的计时器字典（键：计时器唯一标识，值：对应的FrameTimer实例）
        private Dictionary<string, FrameTimer> _frameTimers = new();

        // ------------------------------ 基于时间的计时器操作 ------------------------------

        /// <summary>
        /// 启动或重启一个基于时间的计时器。
        /// </summary>
        /// <param name="key">计时器的唯一标识字符串。</param>
        /// <param name="duration">计时器的持续时间（秒）。</param>
        /// <remarks>
        /// 若 <paramref name="key"/> 已存在，则重启该计时器并更新其持续时间。
        /// 若 <paramref name="key"/> 不存在，则创建一个新的计时器。
        /// </remarks>
        public void Start(string key, float duration)
        {
            if (_timers.ContainsKey(key))
                _timers[key].ReStart(duration);
            else
                _timers[key] = new Timer(duration);
        }

        /// <summary>
        /// 检查指定基于时间的计时器是否已完成。
        /// </summary>
        /// <param name="key">计时器的唯一标识字符串。</param>
        /// <returns>
        /// 若计时器存在且已完成，则返回 <c>true</c>；
        /// 否则（计时器不存在或未完成）返回 <c>false</c>。
        /// </returns>
        public bool IsFinished(string key)
        {
            return _timers.TryGetValue(key, out var timer) && timer.IsFinished;
        }

        /// <summary>
        /// 检查指定基于时间的计时器已流逝时间是否在指定范围内。
        /// </summary>
        /// <param name="key">计时器的唯一标识字符串。</param>
        /// <param name="minTime">范围的最小时间（秒）。</param>
        /// <param name="maxTime">范围的最大时间（秒）。</param>
        /// <returns>
        /// 若计时器存在且已流逝时间在 [<paramref name="minTime"/>, <paramref name="maxTime"/>] 范围内，则返回 <c>true</c>；
        /// 否则返回 <c>false</c>。
        /// </returns>
        public bool IsElapsedInRange(string key, float minTime, float maxTime)
        {
            if (_timers.TryGetValue(key, out var timer))
            {
                float elapsed = timer.Elapsed;
                return elapsed >= minTime && elapsed <= maxTime;
            }
            return false;
        }

        /// <summary>
        /// 获取指定基于时间的计时器已流逝的时间。
        /// </summary>
        /// <param name="key">计时器的唯一标识字符串。</param>
        /// <returns>
        /// 若计时器存在，则返回已流逝时间（秒）；
        /// 若计时器不存在，则返回 <c>0</c>。
        /// </returns>
        public float GetElapsed(string key)
        {
            return _timers.TryGetValue(key, out var timer) ? timer.Elapsed : 0f;
        }

        /// <summary>
        /// 重启指定基于时间的计时器（沿用原有持续时间）。
        /// </summary>
        /// <param name="key">计时器的唯一标识字符串。</param>
        /// <remarks>若计时器不存在，则不执行任何操作。</remarks>
        public void Restart(string key)
        {
            if (_timers.TryGetValue(key, out var timer))
                timer.ReStart(null);
        }

        // ------------------------------ 基于帧的计时器操作 ------------------------------

        /// <summary>
        /// 启动或重启一个基于帧的计时器。
        /// </summary>
        /// <param name="key">计时器的唯一标识字符串。</param>
        /// <param name="maxFrames">计时器的总帧数。</param>
        /// <remarks>
        /// 若 <paramref name="key"/> 已存在，则重启该计时器并更新其总帧数。
        /// 若 <paramref name="key"/> 不存在，则创建一个新的计时器。
        /// </remarks>
        public void Start(string key, int maxFrames)
        {
            if (_frameTimers.ContainsKey(key))
                _frameTimers[key].ReStart(maxFrames);
            else
                _frameTimers[key] = new FrameTimer(maxFrames);
        }

        /// <summary>
        /// 检查指定基于帧的计时器是否已完成。
        /// </summary>
        /// <param name="key">计时器的唯一标识字符串。</param>
        /// <returns>
        /// 若计时器存在且已完成，则返回 <c>true</c>；
        /// 否则（计时器不存在或未完成）返回 <c>false</c>。
        /// </returns>
        public bool IsFrameFinished(string key)
        {
            return _frameTimers.TryGetValue(key, out var frameTimer) && frameTimer.IsFinished;
        }

        /// <summary>
        /// 检查指定基于帧的计时器已流逝帧数是否在指定范围内。
        /// </summary>
        /// <param name="key">计时器的唯一标识字符串。</param>
        /// <param name="minFrames">范围的最小帧数。</param>
        /// <param name="maxFrames">范围的最大帧数。</param>
        /// <returns>
        /// 若计时器存在且已流逝帧数在 [<paramref name="minFrames"/>, <paramref name="maxFrames"/>] 范围内，则返回 <c>true</c>；
        /// 否则返回 <c>false</c>。
        /// </returns>
        public bool IsFrameInRange(string key, int minFrames, int maxFrames)
        {
            if (_frameTimers.TryGetValue(key, out var frameTimer))
            {
                int elapsed = frameTimer.ElapsedFrames;
                return elapsed >= minFrames && elapsed <= maxFrames;
            }
            return false;
        }

        /// <summary>
        /// 获取指定基于帧的计时器已流逝的帧数。
        /// </summary>
        /// <param name="key">计时器的唯一标识字符串。</param>
        /// <returns>
        /// 若计时器存在，则返回已流逝帧数；
        /// 若计时器不存在，则返回 <c>0</c>。
        /// </returns>
        public int GetElapsedFrames(string key)
        {
            return _frameTimers.TryGetValue(key, out var frameTimer) ? frameTimer.ElapsedFrames : 0;
        }

        /// <summary>
        /// 获取指定基于帧的计时器已流逝的时间（秒）。
        /// </summary>
        /// <param name="key">计时器的唯一标识字符串。</param>
        /// <returns>
        /// 若计时器存在，则返回已流逝时间（秒）；
        /// 若计时器不存在，则返回 <c>0</c>。
        /// </returns>
        public float GetElapsedTimeFromFrames(string key)
        {
            return _frameTimers.TryGetValue(key, out var frameTimer) ? frameTimer.ElapsedTime : 0f;
        }

        /// <summary>
        /// 获取指定基于帧的计时器的总持续时间（秒）。
        /// </summary>
        /// <param name="key">计时器的唯一标识字符串。</param>
        /// <returns>
        /// 若计时器存在，则返回总持续时间（秒）；
        /// 若计时器不存在，则返回 <c>0</c>。
        /// </returns>
        public float GetDurationTimeFromFrames(string key)
        {
            return _frameTimers.TryGetValue(key, out var frameTimer) ? frameTimer.DurationTime : 0f;
        }

        /// <summary>
        /// 重启指定基于帧的计时器（沿用原有总帧数）。
        /// </summary>
        /// <param name="key">计时器的唯一标识字符串。</param>
        /// <remarks>若计时器不存在，则不执行任何操作。</remarks>
        public void RestartFrame(string key)
        {
            if (_frameTimers.TryGetValue(key, out var frameTimer))
                frameTimer.ReStart(null);
        }

        // ------------------------------ 通用工具方法 ------------------------------

        /// <summary>
        /// 移除指定标识的计时器。
        /// </summary>
        /// <param name="key">计时器的唯一标识字符串。</param>
        /// <remarks>该方法会同时在基于时间和基于帧的计时器字典中查找并移除。</remarks>
        public void Remove(string key)
        {
            _timers.Remove(key);
            _frameTimers.Remove(key);
        }

        /// <summary>
        /// 检查指定标识的计时器是否存在。
        /// </summary>
        /// <param name="key">计时器的唯一标识字符串。</param>
        /// <returns>
        /// 若该标识的计时器（基于时间或基于帧）存在，则返回 <c>true</c>；
        /// 否则返回 <c>false</c>。
        /// </returns>
        public bool Exists(string key)
        {
            return _timers.ContainsKey(key) || _frameTimers.ContainsKey(key);
        }

        /// <summary>
        /// 清理所有已完成的计时器。
        /// </summary>
        /// <remarks>
        /// 遍历所有基于时间和基于帧的计时器，将已完成的计时器从各自的字典中移除。
        /// 这是一个性能优化方法，可以防止字典无限增长。
        /// </remarks>
        public void CleanupFinished()
        {
            var toRemove = new List<string>();
            foreach (var kvp in _timers)
                if (kvp.Value.IsFinished)
                    toRemove.Add(kvp.Key);
            foreach (var key in toRemove)
                _timers.Remove(key);

            toRemove.Clear();
            foreach (var kvp in _frameTimers)
                if (kvp.Value.IsFinished)
                    toRemove.Add(kvp.Key);
            foreach (var key in toRemove)
                _frameTimers.Remove(key);
        }
    }
}