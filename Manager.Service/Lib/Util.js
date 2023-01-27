const RandomNumber = function (min, max) {
    return Math.floor(Math.random() * (max - min + 1) + min);
}

const IsNullOrUndefined = (value) => ((value === null) || (value === undefined));

const Guid = function () {
    const parts = [
        [0,0,0,0].map(a => RandomNumber(0,255)).map(a => `000${a.toString('16')}`.slice(-2)),
        [0,0].map(a => RandomNumber(0,255)).map(a => `000${a.toString('16')}`.slice(-2)),
        [0,0].map(a => RandomNumber(0,255)).map(a => `000${a.toString('16')}`.slice(-2)),
        [0,0].map(a => RandomNumber(0,255)).map(a => `000${a.toString('16')}`.slice(-2)),
        [0,0,0,0,0,0].map(a => RandomNumber(0,255)).map(a => `000${a.toString('16')}`.slice(-2)),
    ];

    return parts.map(a => a.join('')).join('-');
}

module.exports = {
    RandomNumber,
    IsNullOrUndefined,
    Guid
}
