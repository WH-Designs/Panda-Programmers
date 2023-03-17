import { GetAuthTopArtistsAsync } from '../../MusicCollaborationManager/wwwroot/js/listenerindex.js'

test('data passed to function is correct', () => {
    data = [{"genres": ["pop", "rock"]}, {"genres": ["metal"]}];

    var keys = GetAuthTopArtistsAsync(data);

    expect(keys).toBe(["metal", "pop", "rock"]);
})